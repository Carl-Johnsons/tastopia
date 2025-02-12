from fastapi import FastAPI, File, UploadFile
from fastapi.middleware.cors import CORSMiddleware
from ultralytics import YOLO
from PIL import Image
import io
import uvicorn

# Load the YOLO model
model = YOLO("./model/best.pt")
box_model = YOLO('./model/bestv13.pt')

app = FastAPI(redirect_slashes=False)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # Change this to your frontend domain in production
    allow_credentials=False,
    allow_methods=["*"],
    allow_headers=["*"],
)

names = dict()
for i in open("names.txt").read().splitlines():
    names[i.split('_')[0]] = i.split('_')[1]


@app.post("/predict")
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


@app.post("/predict_box")
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

uvicorn.run(app, host="0.0.0.0", port=5009)