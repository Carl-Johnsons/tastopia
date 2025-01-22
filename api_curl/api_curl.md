# RECIPE

**create recipe**
curl --location 'http://localhost:5000/api/recipe/create-recipe' \
--form 'title="title test 1"' \
--form 'recipeImage=@"/D:/Images/Anime Image/0baca657530fd9c5_9d9622b58f6f65a2_5617415044574976143215.jpg"' \
--form 'description="desciription test 1"' \
--form 'ingredients[0]="1 trai ca"' \
--form 'ingredients[1]="1 muong cafe"' \
--form 'ingredients[2]="2 qua dua leo"' \
--form 'steps[0].ordinalNumber="1"' \
--form 'steps[0].content="step 1 content"' \
--form 'steps[0].Images=@"/D:/Images/Anime Image/2e3321fce05a83cac5945d4283b573fe.jpg"' \
--form 'steps[1].ordinalNumber="2"' \
--form 'steps[1].content="step 2 content"' \
--form 'steps[1].Images=@"/D:/Images/Anime Image/4caa520c43e4a189c0b0208a23ede849.jpg"' \
--form 'tagCodes[0]="code 0"' \
--form 'tagCodes[1]="code 1"' \
--form 'additionTagValues[0]="AdditionTagValues 0"' \
--form 'AdditionTagValues[1]="AdditionTagValues 1"'
**search recipe**
curl --location 'http://localhost:5000/api/recipe/search-recipe' \
--header 'Content-Type: application/json' \
--data '{
"skip" : "0",
"tagValues" : [""],
"keyword" : ""
}'
**get recipe feeds**
curl --location 'http://localhost:5000/api/recipe/get-recipe-feed' \
--header 'Content-Type: application/json' \
--data '{
"skip" : "0",
"tagvalues" : ["All"]
}'
**get tags**
curl --location 'http://localhost:5000/api/recipe/get-tag' \
--header 'Content-Type: application/json' \
--data '{
"skip" : 0,
"tagCodes" : ["ALL"],
"keyword" : "",
"category" : "INGREDIENT"
}'
**vote recipe**
curl --location 'http://localhost:5000/api/recipe/vote-recipe' \
--header 'Content-Type: application/json' \
--data '{
"isUpvote" : false,
"recipeId" : "d2189f90-6991-4901-8195-f0c12d24d900"
}'
**get recipe details**
curl --location 'http://localhost:5000/api/recipe/get-recipe-details' \
--header 'Content-Type: application/json' \
--data '{
"recipeId" : "3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"
}'
**get recipe comments**
curl --location 'http://localhost:5000/api/recipe/get-recipe-comments' \
--header 'Content-Type: application/json' \
--data '{
"recipeId": "d2189f90-6991-4901-8195-f0c12d24d900",
"skip": 0
}'

**comment recipe**
curl --location 'http://localhost:5000/api/recipe/comment-recipe' \
--header 'Content-Type: application/json' \
--data '{
"recipeId" : "d2189f90-6991-4901-8195-f0c12d24d900",
"content" : "This recipe is absolutely delicious!"
}'

**get recipe steps**
curl --location 'https://localhost:7000/api/recipe/get-recipe-steps' \
--header 'Content-Type: application/json' \
--data '{
"recipeId" : "d2189f90-6991-4901-8195-f0c12d24d900"
}'

**bookmark recipe**
curl --location 'https://localhost:7000/api/recipe/bookmark-recipe' \
--header 'Content-Type: application/json' \
--data '{
"recipeId" : "aa626791-ee53-4390-a5a5-94c5b8096f87"
}'

**get recipe bookmark**
curl --location 'https://localhost:7000/api/recipe/get-recipe-bookmarks' \
--header 'Content-Type: application/json' \
--data '{
"skip" : "0"
}'

# USER

**search user**
curl --location 'http://localhost:5003/api/user/search' \
--header 'Content-Type: application/json' \
--data '{
"keyword" : "admin",
"skip" : 0
}'

**get current user**
curl --location 'http://localhost:5003/api/user/get-current-user-details' \
