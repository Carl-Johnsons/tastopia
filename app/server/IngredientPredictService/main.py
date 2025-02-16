import asyncio
import json
from fastapi import FastAPI, File, UploadFile, WebSocket, WebSocketDisconnect
from contextlib import asynccontextmanager
from ultralytics import YOLO
from PIL import Image
import logging
import random
import httpx
import logging.config
import yaml
import io
import uvicorn

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

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
    # Read image
    image = Image.open(io.BytesIO(await file.read()))

    # Run YOLO detection
    results = model(image, verbose=False)

    # Parse results
    classifications = []
    for result in results:
        for cls, conf in zip(result.probs.top5, result.probs.top5conf):
            classifications.append({
                "class": result.names[cls],
                "confidence": float(conf),
                "name": names[result.names[cls]]
            })

    return {"classifications": classifications}

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

@app.websocket("/ws/video")
async def processVideo(websocket: WebSocket):
    await websocket.accept()
    try:
        while True:
            # Receive a frame as binary data.
            frame_bytes = await websocket.receive_bytes()
            try:
                # Convert the binary frame to an image.
                image = Image.open(io.BytesIO(frame_bytes))
            except Exception as e:
                # If the frame data is invalid, send an error back.
                await websocket.send_text(json.dumps({"error": f"Invalid image data: {str(e)}"}))
                continue
            
            # Process the image with your model (e.g., YOLO detection).
            results = model(image, verbose=False)
            predictions = []
            for result in results:
                for cls, conf in zip(result.probs.top5, result.probs.top5conf):
                    predictions.append({
                        "class": result.names[cls],
                        "confidence": float(conf),
                        "name": names[result.names[cls]]
                    })

            boxResults = box_model(image, verbose=False)
            
            # Send back the prediction for this frame.
            await websocket.send_text(json.dumps({"classifications": predictions,
                                                  "boxes": boxResults[0].boxes.xyxyn.tolist()}))
            
            # Optionally throttle the frame rate.
            await asyncio.sleep(0.1)
    except WebSocketDisconnect:
        print("Client disconnected")


@app.post("/api/ingredient-predict/box")
async def predict(file: UploadFile = File(...)):
    # Read image
    image = Image.open(io.BytesIO(await file.read()))

    # image.show()

    # Run YOLO detection
    results = box_model(image, verbose=False)

    # print(results[0].boxes)

    return {"boxes": results[0].boxes.xyxyn.tolist()}


@app.get("/")
async def root():
    return {"message": "YOLO FastAPI is running!"}

uvicorn.run(app, host="0.0.0.0", port=5009,log_config=log_config)