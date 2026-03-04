import tensorflow as tf
from open_clip import create_model_from_pretrained, get_tokenizer
import logging

_model = None
_clip_model = None
_clip_preprocessor = None
_tokenizer = None
_model_path = '/models/convnext_224_f.model.keras'

def get_model():
  global _model
  if(_model is None):
    _load_model()
  return _model

def get_clip_model():
  global _clip_model
  if(_clip_model is None):
    _load_clip_model()
  return _clip_model

def get_clip_preprocessor():
  global _clip_preprocessor
  if(_clip_preprocessor is None):
    _load_clip_model()
  return _clip_preprocessor

def get_model_tokenizer():
  global _tokenizer
  if(_tokenizer is None):
    _load_tokenizer()
  return _tokenizer

def _load_model():
  global _model
  logging.info(f"Loading model from path {_model_path}")
  _model = tf.keras.models.load_model(_model_path)
  logging.info("Model loaded successfully!")

def _load_clip_model():
  global _clip_model, _clip_preprocessor
  logging.info(f"Loading clip model")
  _clip_model, _clip_preprocessor = create_model_from_pretrained('hf-hub:apple/DFN5B-CLIP-ViT-H-14')
  logging.info("Clip model loaded successfully!")


def _load_tokenizer():
  global _tokenizer
  logging.info(f"Loading tokenizer")
  _tokenizer = get_tokenizer('ViT-H-14')
  logging.info("Tokenizer loaded successfully!")
