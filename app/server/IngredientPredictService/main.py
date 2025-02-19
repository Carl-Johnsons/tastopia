import asyncio
import json
from fastapi import FastAPI, File, UploadFile, WebSocket, WebSocketDisconnect
from contextlib import asynccontextmanager
from ultralytics import YOLO
from PIL import Image
import logging
import time
import random
import httpx
import logging.config
import yaml
import io
import uvicorn
from ConnectionManager import ConnectionManager

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

connection_manager = ConnectionManager()

# Load the YOLO model
model = YOLO("./model/best.pt")
box_model = YOLO('./model/bestv13.pt')

@asynccontextmanager
async def lifespan(app: FastAPI):
    service_name = "ingredient-predict-service"
    service_id = service_name + str(random.randint(100000, 999999))
    consul_scheme = "http"
    consul_port = 8500
    consul_host = "localhost"
    consul_base_address = f"{consul_scheme}://{consul_host}:{consul_port}"

    consul_register_url = f"{consul_base_address}/v1/agent/service/register"
    consul_deregister_url = f"{consul_base_address}/v1/agent/service/deregister/{service_id}"

    logging.info(f"Start instance {service_id}")
    service_registration = {
        "ID": service_id,
        "Name": service_name,
        "Address": "localhost",
        "Port": 5009,
        "Tags": ["fastapi", "python"],
        "Check": {
            "HTTP": "http://host.docker.internal:5009/health",
            "Interval": "10s"
        }
    }
    async with httpx.AsyncClient() as client:
        response = await client.put(consul_register_url, json=service_registration)
        if response.status_code == 200:
            logging.info("Successfully registered with Consul 😊")
        else:
            logging.error("Failed to register with Consul", response.text)
    yield  # The app starts here

    async with httpx.AsyncClient() as client:
        response = await client.put(consul_deregister_url)
        if response.status_code == 200:
            logging.info("Deregistered from Consul 👋")
        else:
            logging.error("Deregistration failed:", response.text)

app = FastAPI(lifespan=lifespan, redirect_slashes=False)

names = dict()
for i in open("names.txt").read().splitlines():
    names[i.split('_')[0]] = i.split('_')[1]

@app.get("/health")
async def health():
    return {"status": "ok"}


@app.post("/api/ingredient-predict")
async def predict(file: UploadFile = File(...)):
    image = Image.open(io.BytesIO(await file.read()))
    image = image.rotate(270, expand=True)

    results = model(image, verbose=False)

    classifications = []
    for result in results:
        for cls, conf in zip(result.probs.top5, result.probs.top5conf):
            classifications.append({
                "class": result.names[cls],
                "confidence": float(conf),
                "name": names[result.names[cls]]
            })

    results = box_model(image, verbose=False)
    return {"classifications": classifications, "boxes": results[0].boxes.xyxyn.tolist()}


@app.websocket("/ws")
async def websocket_endpoint(websocket: WebSocket):
    await websocket.accept()
    try:
        while True:
            data = await websocket.receive_text()
            # Echo the data back, for instance
            await websocket.send_text(f"Echo: {data}")
    except WebSocketDisconnect:
        print("Client disconnected")

@app.websocket("/ws/video/{user_id}")
async def processVideo(websocket: WebSocket,user_id: str):
    await connection_manager.connect(websocket, user_id)
    try:
        while True:
            start = time.perf_counter()

            # Receive frame bytes.
            message = await websocket.receive()

            if message["type"] == "websocket.disconnect":
                print("WebSocket disconnected.")
                break
            if "ping" in message:
                logging.info("Client pinging: Healthy")
            elif "bytes" in message:
                frame_bytes = message["bytes"]
                recv_time = time.perf_counter()

                try:
                    # Convert the binary frame to an image.
                    image = Image.open(io.BytesIO(frame_bytes))
                    image.load()  # Ensure the image data is fully loaded.
                except Exception as e:
                    await websocket.send_text(
                        json.dumps({"error": f"Invalid image data: {str(e)}"})
                    )
                    continue
                conv_time = time.perf_counter()

                # Process the image with your model.
                results = model(image, verbose=False)
                model_time = time.perf_counter()

                predictions = []
                for result in results:
                    for cls, conf in zip(result.probs.top5, result.probs.top5conf):
                        predictions.append({
                            "class": result.names[cls],
                            "confidence": float(conf),
                            "name": names[result.names[cls]]
                        })

                boxResults = box_model(image, verbose=False)
                box_time = time.perf_counter()

                # Optionally send back the prediction for this frame.
                await websocket.send_text(json.dumps({
                    "classifications": predictions,
                    "boxes": boxResults[0].boxes.xyxyn.tolist()
                }))

                end = time.perf_counter()

                # Log timings.
                print(f"Receive Time: {recv_time - start:.4f} sec")
                print(f"Conversion Time: {conv_time - recv_time:.4f} sec")
                print(f"Model Inference Time: {model_time - conv_time:.4f} sec")
                print(f"Box Model Time: {box_time - model_time:.4f} sec")
                print(f"Total Processing Time: {end - start:.4f} sec\n")

                # Optionally throttle the frame rate.
                await asyncio.sleep(0.1)
            else:
                logging.info("No binaries data or ping data received")
    except WebSocketDisconnect:
        connection_manager.disconnect(websocket, user_id)
        print("Client disconnected")

@app.get("/")
async def root():
    return {"message": "YOLO FastAPI is running!"}

uvicorn.run(app, host="0.0.0.0", port=5009,log_config=log_config)
