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

# USER

**search user**
curl --location 'http://localhost:5003/api/user/search' \
--header 'Content-Type: application/json' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkEwNjY2M0NDOTMxNjREQ0QxRDBDMTA0NjAwQURFRjNBIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEiLCJuYmYiOjE3MzU4OTEwNDAsImlhdCI6MTczNTg5MTA0MCwiZXhwIjoxNzM4NDgzMDQwLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvcmVzb3VyY2VzIiwic2NvcGUiOlsiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsInBob25lIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXSwiY2xpZW50X2lkIjoicmVhY3QubmF0aXZlIiwic3ViIjoiNjFjNjFhYzctMjkxZS00MDc1LTk2ODktNjY2ZWYwNTU0N2VkIiwiYXV0aF90aW1lIjoxNzM1ODkxMDQwLCJpZHAiOiJsb2NhbCIsImdpdmVuX25hbWUiOiJhbGljZSIsInBob25lX251bWJlciI6IjAxMjM0NTY3ODkiLCJlbWFpbCI6IkFsaWNlU21pdGhAZW1haWwuY29tIiwicm9sZSI6IlVTRVIiLCJqdGkiOiJGRDQ3Q0ZFNzBBQTM4MEZDRUEzOTkxODkyOTNFMEZCRSJ9.gdKndsWsfhEOc1y2Ao2zOv-YDa6ANmhCRyjn4QPaVo6Weld0Q-OQG1aHYlllFDY3H0h8olnClLRK2bDHGNvIFX*Npza93F2gqdQGYzP1w1KbjuO2R7m0wZB5WVLGqR4tGxfmMHHgbwYAuvfhmHbTJ2hS0kJIKESXAQb7z9lkcNy0ZOMAdcQqbST1-VyxW0lD63azRNW8J-1o8ivCOH-sfhJfFL_oIh3R_7cFOzZJeAVUDrMpOWCbc_rpDfjTAcRGn8zuMaCxu5dccB9WTSo76glV0gGPGC91Xv6PBdNsTXbjlLz7L03hZTUJSCxYJUrYlCfhunSbA1ujS33jaViCNQ' \
--header 'Cookie: .AspNetCore.Identity.Application=CfDJ8EBdLEPnnJ9KsRFMazgNrCfBYzhdkVBzVxb22XZTJBhI6pAt-Vkcje_H9s55E0z4FmfIhbYx16jPsfXEWTTAoOSZ1clD0IaUU-br_5C-92ya1HlCvnRJNHNpFwJWRZ_BKOQn7IQL2XoaHF-AGyBl0tKrVTUX_xs7ZR69AzglN2GwMvwhjpMdZmULtaTrWhYEGQwvXPU0HM_Lkv7F7irlOpzsIpYGn_fZDjErgganqNW4c1EqSfmwdyRVr1nJWxLtJcv4-sWz_zj5zEWJjOXZ7u7BvVNVHrS8SmxOCC-hSm2uHsBLxCqLpTvHeAqULlvGv2ZJfb6G0gI2RnYu-0aqGmv3x221xcSTP80m2izoDML8qMrWIJmNKZWt93NhZOaEoaevoPdtWRAJfj198CUic7NY5fC6nbQiX7xSY3UJetZ0r9yFoTPExeGMzm4er0zD2fhh2ZsKWzEaaYyJsGMOq0CxC_ROCuvbmStTE1O3xcqgu6VCfIPNcKVnPGFhK_Nugo2TxQwuadELGE1bN6I-BR4qOm6pgwkA3PzXJuMWXtPqtvaFdpoJpCvMO2VHEve8dx3HUHnXRdo06HV-ZFq7VdxkBoC9MD5-MnCVZ-S23H75sYNdnU5XXPepVlBlCXD571KQhU0wI4UpsjfuRUCRKBE0ZfMnNjAFwC_oqHpS24ftjNzphUtO*-\_-kRXffmGIMvovN6SE6PJuDHnO6RjUZwOCf8oddRTkFONgBaztRBWzwrSa7I_CjMFHBsYe-b4dCLWTXWArW26FGk_gQEghDrV3rnn3jCnIDsXcw-sgUHAU58PCP8JvbAB5Mf9_Sf91VfniVxRCUai-64wgMhxsHmBE-XvYi7CEWqTSAEGAQJrBVNvyCC_fVGq6YbCmwYyb7NHfAFgEkD5REPyfmWuYTXGR3pHqjJ_mELxBnMm2THoBwxXevHb5v6nRr8NxHSGe9ReWuwLqKQndEv2WllsJYG5d5Zp6lW8h5mqKeZlJK3dKAnjGpUxdZNVEk2QNOXIaLBcb-CdclY6Je85eg-Tl6xLaMc5Y2omUzJoCwnjLTe0Zgx1E8U0zhyrNwf9j2QzirQ; idsrv.session=6FD6ECBD3CC5EFD4322E0B80AA3622F4' \
--data '{
"keyword" : "admin",
"skip" : 0
}'

**get current user**
curl --location 'http://localhost:5003/api/user/get-current-user-details' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkEwNjY2M0NDOTMxNjREQ0QxRDBDMTA0NjAwQURFRjNBIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEiLCJuYmYiOjE3MzU4OTEwNDAsImlhdCI6MTczNTg5MTA0MCwiZXhwIjoxNzM4NDgzMDQwLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEvcmVzb3VyY2VzIiwic2NvcGUiOlsiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsInBob25lIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXSwiY2xpZW50X2lkIjoicmVhY3QubmF0aXZlIiwic3ViIjoiNjFjNjFhYzctMjkxZS00MDc1LTk2ODktNjY2ZWYwNTU0N2VkIiwiYXV0aF90aW1lIjoxNzM1ODkxMDQwLCJpZHAiOiJsb2NhbCIsImdpdmVuX25hbWUiOiJhbGljZSIsInBob25lX251bWJlciI6IjAxMjM0NTY3ODkiLCJlbWFpbCI6IkFsaWNlU21pdGhAZW1haWwuY29tIiwicm9sZSI6IlVTRVIiLCJqdGkiOiJGRDQ3Q0ZFNzBBQTM4MEZDRUEzOTkxODkyOTNFMEZCRSJ9.gdKndsWsfhEOc1y2Ao2zOv-YDa6ANmhCRyjn4QPaVo6Weld0Q-OQG1aHYlllFDY3H0h8olnClLRK2bDHGNvIFX*Npza93F2gqdQGYzP1w1KbjuO2R7m0wZB5WVLGqR4tGxfmMHHgbwYAuvfhmHbTJ2hS0kJIKESXAQb7z9lkcNy0ZOMAdcQqbST1-VyxW0lD63azRNW8J-1o8ivCOH-sfhJfFL_oIh3R_7cFOzZJeAVUDrMpOWCbc_rpDfjTAcRGn8zuMaCxu5dccB9WTSo76glV0gGPGC91Xv6PBdNsTXbjlLz7L03hZTUJSCxYJUrYlCfhunSbA1ujS33jaViCNQ' \
--header 'Cookie: .AspNetCore.Identity.Application=CfDJ8EBdLEPnnJ9KsRFMazgNrCfBYzhdkVBzVxb22XZTJBhI6pAt-Vkcje_H9s55E0z4FmfIhbYx16jPsfXEWTTAoOSZ1clD0IaUU-br_5C-92ya1HlCvnRJNHNpFwJWRZ_BKOQn7IQL2XoaHF-AGyBl0tKrVTUX_xs7ZR69AzglN2GwMvwhjpMdZmULtaTrWhYEGQwvXPU0HM_Lkv7F7irlOpzsIpYGn_fZDjErgganqNW4c1EqSfmwdyRVr1nJWxLtJcv4-sWz_zj5zEWJjOXZ7u7BvVNVHrS8SmxOCC-hSm2uHsBLxCqLpTvHeAqULlvGv2ZJfb6G0gI2RnYu-0aqGmv3x221xcSTP80m2izoDML8qMrWIJmNKZWt93NhZOaEoaevoPdtWRAJfj198CUic7NY5fC6nbQiX7xSY3UJetZ0r9yFoTPExeGMzm4er0zD2fhh2ZsKWzEaaYyJsGMOq0CxC_ROCuvbmStTE1O3xcqgu6VCfIPNcKVnPGFhK_Nugo2TxQwuadELGE1bN6I-BR4qOm6pgwkA3PzXJuMWXtPqtvaFdpoJpCvMO2VHEve8dx3HUHnXRdo06HV-ZFq7VdxkBoC9MD5-MnCVZ-S23H75sYNdnU5XXPepVlBlCXD571KQhU0wI4UpsjfuRUCRKBE0ZfMnNjAFwC_oqHpS24ftjNzphUtO*-\_-kRXffmGIMvovN6SE6PJuDHnO6RjUZwOCf8oddRTkFONgBaztRBWzwrSa7I_CjMFHBsYe-b4dCLWTXWArW26FGk_gQEghDrV3rnn3jCnIDsXcw-sgUHAU58PCP8JvbAB5Mf9_Sf91VfniVxRCUai-64wgMhxsHmBE-XvYi7CEWqTSAEGAQJrBVNvyCC_fVGq6YbCmwYyb7NHfAFgEkD5REPyfmWuYTXGR3pHqjJ_mELxBnMm2THoBwxXevHb5v6nRr8NxHSGe9ReWuwLqKQndEv2WllsJYG5d5Zp6lW8h5mqKeZlJK3dKAnjGpUxdZNVEk2QNOXIaLBcb-CdclY6Je85eg-Tl6xLaMc5Y2omUzJoCwnjLTe0Zgx1E8U0zhyrNwf9j2QzirQ; idsrv.session=6FD6ECBD3CC5EFD4322E0B80AA3622F4'
