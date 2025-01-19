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

**update recipe**
lưu ý:

-chỗ recipeImage nếu giữ nguyên không muốn update thì gán bằng null hoặc k tạo trong request còn muốn update thì gán giá trị cho nó.
-chỗ các steps phải có stepId (steps[n].stepId) nếu như update 1 step cũ, còn thêm 1 step mới thì không cần tạo steps[n].stepId trong request
-các step đã tồn tại sẽ bị xóa nếu như trong request không tìm thấy stepId của nó.

curl --location 'https://localhost:7000/api/recipe/update-recipe' \
--form 'id="b74abbbf-5238-4aff-8154-2144656be675"' \
--form 'title="Mì trộn trứng chiên update"' \
--form 'recipeImage=@"/D:/Images/Anime Image/88f88b7fca92bb8d_93981df87b63ba1d_6975815044573808143215.jpg"' \
--form 'description="desciription Mì trộn update"' \
--form 'ingredients[0]="1 quả trứng update"' \
--form 'ingredients[1]="1 gói mì"' \
--form 'ingredients[2]="1 muỗng nước tương update"' \
--form 'steps[0].stepId="04af3c40-2d2a-4f82-8979-c0f2a9a15e82"' \
--form 'steps[0].ordinalNumber="1"' \
--form 'steps[0].content="step 1 mì trộn"' \
--form 'steps[0].Images=@"/D:/Images/Anime Image/2e3321fce05a83cac5945d4283b573fe.jpg"' \
--form 'steps[0].Images=@"/D:/Images/Anime Image/4caa520c43e4a189c0b0208a23ede849.jpg"' \
--form 'steps[1].stepId="b976ba2a-3468-4af5-b717-6dc9a2e6eb2f"' \
--form 'steps[1].ordinalNumber="2"' \
--form 'steps[1].content="step mi trộn update"' \
--form 'steps[1].Images=@"/D:/Images/Anime Image/735279.png"' \
--form 'steps[1].deleteUrls[0]="http://res.cloudinary.com/dhphzuojz/image/upload/v1737285028/file_storage/c89d9023-b1b2-4a75-830b-b1a8f5bec0ac.jpg"' \
--form 'steps[1].deleteUrls[1]="http://res.cloudinary.com/dhphzuojz/image/upload/v1737285028/file_storage/8dc93c2a-ee57-4989-9333-7510193d54b8.jpg"' \
--form 'TagValues[0]="value 0"' \
--form 'TagValues[1]="value 1"'

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
