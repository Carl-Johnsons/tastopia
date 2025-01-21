#!/bin/bash

. ./scripts/lib.sh

project_root=$(pwd)

if [ -f ./doc/plantuml.jar ]; then
    printf "${CYAN}plantuml.jar already existed, skip download!${NC}\n"
else
    printf "${CYAN}Downloading plantuml ...\n\n${NC}"
    curl -L -o ./doc/plantuml.jar https://github.com/plantuml/plantuml/releases/download/v1.2025.0/plantuml-1.2025.0.jar
    printf "\n\t*** ${GREEN}DOWNLOAD PLANTUML FILE${NC} ***\n\n"
fi

function removeImagesInDir() {
    local dir=$1
    find $dir -type f \( -iname \*.png -o -iname \*.svg \) -exec rm -f {} +
}

function progress_bar() {
    local current=$1
    local total=$2
    local width=50 # Progress bar width
    local progress=$((current * width / total))
    local remaining=$((width - progress))
    printf "\r["
    printf "%0.s#" $(seq 1 $progress)
    printf "%0.s-" $(seq 1 $remaining)
    printf "] $current/$total"
}

function exportsImageInDir() {
    local dir=$1
    removeImagesInDir $dir

    local total_files=$(find $dir -type f -iname "*.plantuml" | wc -l)
    local current=0

    find $dir -type f -iname "*.plantuml" | while read plantuml_file; do
        java -jar ./doc/plantuml.jar -tpng "$plantuml_file"
        ((current++))
        progress_bar $current $total_files
    done

    # Process the SVG files and show the progress
    current=0
    find $dir -type f -iname "*.plantuml" | while read plantuml_file; do
        java -jar ./doc/plantuml.jar -tsvg "$plantuml_file"
        ((current++))
        progress_bar $current $total_files
    done

    echo # To move to the next line after progress
}

# Main function to process all directories
function process_all_directories() {
    directories=(
        "./doc/SDS/Identity"
        "./doc/SDS/Recipe"
        "./doc/SDS/Tracking"
        "./doc/SDS/User"
    )

    total_dirs=${#directories[@]}
    current_dir=1

    for dir in "${directories[@]}"; do
        echo "Processing $dir ($current_dir/$total_dirs)"
        exportsImageInDir "$dir"
        ((current_dir++))
    done
}

# Run the process
process_all_directories
