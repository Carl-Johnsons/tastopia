#!/bin/bash

. ./scripts/lib.sh

output_dir="./doc/plantuml/combined_project"

rm -rf $output_dir

gen_class_diagram() {
  local input_paths=$1
  local output_dir=$2
  puml-gen "$input_paths" "$output_dir" -excludePaths "bin,obj,Properties" -dir -createAssociation -allInOne
}

# List all project directories you want to include
project_paths=(
  "./app/server/RecipeService/src/RecipeService.API"
  "./app/server/RecipeService/src/RecipeService.Infrastructure"
  "./app/server/RecipeService/src/RecipeService.Application"
  "./app/server/RecipeService/src/RecipeService.Domain"
)

gen_class_diagram "./app/server/RecipeService/src/RecipeService.API" "$output_dir"
gen_class_diagram "./app/server/RecipeService/src/RecipeService.Infrastructure" "$output_dir"
gen_class_diagram "./app/server/RecipeService/src/RecipeService.Application" "$output_dir"
gen_class_diagram "./app/server/RecipeService/src/RecipeService.Domain" "$output_dir"
