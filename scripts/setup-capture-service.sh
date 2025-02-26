#!/bin/bash

. ./scripts/lib.sh

# Set project root directory
project_root=$(pwd)

cd $project_root/app/server/IngredientPredictService

echo -e "${PURPLE}Create virtual environment for Python${NC}"
python -m venv venv

echo -e "${PURPLE}Activate virtual environment for Python${NC}"

if [[ "$PLATFORM" == "windows" ]]; then
  ./venv/Scripts/activate
else
  source ./venv/bin/activate
fi

echo -e "${PURPLE}Install requirements for Python${NC}"
pip install -r requirements.txt

cd $project_root
