using RecipeService.Domain.Entities;
namespace RecipeService.Infrastructure.Persistence.Mockup.Data;
public class TagData
{
    public static List<Tag> Data = [
        new Tag
        {
            Id = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292"),
            Code = "TOMATO",
            Value = "Tomato",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/3NovRt2.png",
        },
        new Tag
        {
            Id = Guid.Parse("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
            Code = "EGG",
            Value = "Egg",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/BAT5qyL.png",
        },
        new Tag
        {
            Id = Guid.Parse("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
            Code = "RICE",
            Value = "Rice",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/C4nNmU1.png",
        },
        new Tag
        {
            Id = Guid.Parse("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
            Code = "MUSHROOM",
            Value = "Mushroom",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/m8wBuYO.png",
        },
        // Additional Tags for Recipes
        new Tag
        {
            Id = Guid.Parse("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
            Code = "MILK",
            Value = "Milk",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/Rk3MwdQ.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
            Code = "BUTTER",
            Value = "Butter",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/Z8y4Hsr.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
            Code = "CHEESE",
            Value = "Cheese",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/feglS7k.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
            Code = "BACON",
            Value = "Bacon",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/lyYgVRi.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
            Code = "GARLIC",
            Value = "Garlic",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/oLwdHvx.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
            Code = "CARROT",
            Value = "Carrot",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/DZEq7TK.jpg",
        },
        new Tag
        {
            Id = Guid.Parse("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
            Code = "BROCCOLI",
            Value = "Broccoli",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/8nDcffy.png",
        },
        new Tag
        {
            Id = Guid.Parse("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
            Code = "SOY_SAUCE",
            Value = "Soy Sauce",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.imgur.com/2QiWJWH.jpg",
        },

        new Tag
        {
            Id = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce"),
            Code = "BEEF",
            Value = "Beef",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.lovefoodhatewaste.com/sites/default/files/styles/open_graph_image/public/2022-08/Beef-sh344681603.jpg.webp?itok=bXWzTuPi",
        },
        new Tag
        {
            Id = Guid.Parse("6adc1ab3-b10a-4341-881b-553fb7860cc4"),
            Code = "RED_PEPPER",
            Value = "Red pepper",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.producemarketguide.com/media/user_5q6Kv4eMkN/176/red-bell-peppers_variety-page.png",
        },
        //Tag model
        new Tag
        {
            Id = Guid.Parse("70033b9c-071b-451e-a9c0-6f182df45955"),
            Code = "CABBAGE",
            Value = "Cabbage",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://images.healthshots.com/healthshots/en/uploads/2021/09/29144251/cabbage.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("608b9c05-c5f1-40ca-8bb9-8a1dd76f6d81"),
            Code = "PURPLE_CABBAGE",
            Value = "Purple Cabbage",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn.nhathuoclongchau.com.vn/unsafe/800x0/filters:quality(95)/https://cms-prod.s3-sgn09.fptcloud.com/tim_hieu_an_bap_cai_tim_co_tot_khong_a83f26df1e.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("629d1467-b4e4-4910-9203-813216a43db3"),
            Code = "PUMPKIN",
            Value = "Pumpkin",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i0.wp.com/post.medicalnewstoday.com/wp-content/uploads/sites/3/2019/11/a-bunch-of-pumpkins-in-a-box.jpg?w=1155&h=1734"
        },
        new Tag
        {
            Id = Guid.Parse("d8270958-d306-421f-9321-fa8ce66a8e95"),
            Code = "CAULIFLOWER",
            Value = "Cauliflower",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i0.wp.com/post.healthline.com/wp-content/uploads/2020/02/broccoli-cauliflower-1296x728-header.jpg?w=1155&h=1528"
        },
        new Tag
        {
            Id = Guid.Parse("264aece2-1dd3-4326-87a0-40e22d913b47"),
            Code = "CHERRY_TOMATOES",
            Value = "Cherry Tomatoes",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://thomasjosephbutchery.co.uk/cdn/shop/products/organic-seasonal-cherry-tomatoes-113193.jpg?v=1643924230"
        },
        new Tag
        {
            Id = Guid.Parse("7437d1c2-fcc0-43f7-9719-a386def772a1"),
            Code = "SALMON",
            Value = "Salmon",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn-assets-eu.frontify.com/s3/frontify-enterprise-files-eu/eyJvYXV0aCI6eyJjbGllbnRfaWQiOiJmcm9udGlmeS1maW5kZXIifSwicGF0aCI6ImloaC1oZWFsdGhjYXJlLWJlcmhhZFwvZmlsZVwvSGhleHdSaUVCYWJ0b1dFRWpUM1EuanBnIn0:ihh-healthcare-berhad:6Zk6UuetaajSDB-43bdLAoamTKKBCqQFMfjY38nWPbk?width={width}"
        },
        new Tag
        {
            Id = Guid.Parse("7d6c2d1c-11fe-4c4a-a87e-2a395c07d834"),
            Code = "SNAKEHEAD_FISH",
            Value = "Snakehead Fish",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.btaskee.com/wp-content/uploads/2023/08/ca-loc-nuong-mo-hanh-la-su-lua-chon-khi-ban-muon-nau-mon-ngon-tu-ca-loc.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("0e2c473b-1a72-4f52-a2f3-ce399425f185"),
            Code = "EGGPLANT",
            Value = "Eggplant",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.bradleysmoker.com/cdn/shop/articles/Smoked-Marinated-Eggplant-Recipe-scaled.jpg?v=1675739133&width=1500"
        },
        new Tag
        {
            Id = Guid.Parse("31dc282f-d553-43f6-8ba4-ca050e1b8343"),
            Code = "CATFISH",
            Value = "Catfish",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn.tgdd.vn/Files/2020/11/30/1310387/10-mon-ngon-tu-ca-tre-ma-khong-mot-ai-co-the-che-duoc-vi-chat-luong-qua-tuyet-voi-202011301346232292.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("26529e4b-0504-4da6-aa2c-b90232d8ff68"),
            Code = "CELERY",
            Value = "Celery",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://image.slidesdocs.com/responsive-images/background/vibrant-green-celery-stalks-placed-on-a-textured-dark-powerpoint-background_a55d48c3c9__960_540.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("22ddf95e-d898-413d-bad9-945e5cf8a635"),
            Code = "WHITE_RADISH",
            Value = "White Radish",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.vinmec.com/static/uploads/20210125_135257_451549_cu_cai_max_1800x1800_jpg_5a0448abb2.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("29e47c35-09a0-4b8e-9a0b-066ef14e5921"),
            Code = "BANANA",
            Value = "Banana",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://png.pngtree.com/thumb_back/fw800/background/20240601/pngtree-bananas-on-wooden-table-with-green-banana-leaf-background-image_15736671.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("d653e6fc-200b-478b-a593-0cfc490de97f"),
            Code = "OKRA",
            Value = "Okra",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://static.toiimg.com/thumb/msid-118357092,width-1280,height-720,resizemode-4/118357092.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("8dfdfff4-ce63-42c5-aa2c-dc7b4904b943"),
            Code = "LONG_BEAN",
            Value = "Long Bean",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://media.istockphoto.com/id/605957342/photo/sliced-yard-long-bean-vegetable-on-wooden-block.jpg?s=612x612&w=0&k=20&c=RgzilzeHIYRGFv5ixAUMEhnNIMX9yPxcbgwAyIYu9ys="
        },
        new Tag
        {
            Id = Guid.Parse("23d9ad45-e302-431d-9b0f-66b3b5ab0e73"),
            Code = "STRING_BEAN",
            Value = "String Bean",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn.nhathuoclongchau.com.vn/unsafe/800x0/https://cms-prod.s3-sgn09.fptcloud.com/Me_bau_an_dau_cove_duoc_khong_an_bao_nhieu_la_phu_hop_3_9164a4f648.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("c54f00ae-2064-4edb-b246-9da7f45a2735"),
            Code = "STRAWBERRY",
            Value = "Strawberry",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://extension.colostate.edu/wp-content/uploads/2021/04/07000-fig1.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("c1eec9b0-b2fa-46c4-a367-12424208a0a8"),
            Code = "HYACINTH_BEAN",
            Value = "Hyacinth Bean",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://halan.net/wp-content/uploads/2024/03/dac-diem-sinh-truong-cua-dau-van.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("5c9850d8-414b-4250-8fca-8eecaaa09ece"),
            Code = "PAPAYA",
            Value = "Papaya",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://ujamaaseeds.com/cdn/shop/files/PAPAYA_TREE_3.jpg?v=1719490058"
        },
        new Tag
        {
            Id = Guid.Parse("5fd67837-875d-4c0a-a1fb-3a2c84e0d712"),
            Code = "PINEAPPLE",
            Value = "Pineapple",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn.unityfitness.vn/2024/11/tac-dung-cua-trai-thom-doi-voi-phu-nu-1.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("613b4247-4360-42ad-b866-8619c694598f"),
            Code = "WATERMELON",
            Value = "Watermelon",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://static.vecteezy.com/system/resources/previews/004/850/971/large_2x/watermelon-slice-on-wood-background-close-up-fresh-watermelon-pieces-tropical-summer-fruit-free-photo.JPG"
        },
        new Tag
        {
            Id = Guid.Parse("46fc8651-4374-4bbe-b695-95dd698fd341"),
            Code = "CANTALOUPE",
            Value = "Cantaloupe",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn.tgdd.vn/Files/2020/06/20/1264372/cong-dung-cua-dua-luoi-cac-loai-tren-thi-truong-v-11.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("3ad3bd53-aee7-4179-b879-5d6a9dfd3d7d"),
            Code = "PIG_TAIL",
            Value = "Pig's Tail",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://media.istockphoto.com/id/1304387749/photo/fresh-pig-tails-fresh-pork-bones.jpg?s=612x612&w=0&k=20&c=dZH57-6dtLjQLz_Idp6kEhMDekA3HnWnWzvlLH9L2_I="
        },
        new Tag
        {
            Id = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6"),
            Code = "CHICKEN",
            Value = "Chicken",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.allrecipes.com/thmb/qhJDqRRX1CkzlBJ9YDB7BME-vsg=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Raw-Chicken-Mistakes-3x2-1-8a51428f2d5d46f3bc4dce8a5d1f32d3.png"
        },
        new Tag
        {
            Id = Guid.Parse("c6e9d13a-c6fb-456b-8f4b-90912b91ed7e"),
            Code = "BLACK_CHICKEN",
            Value = "Black Chicken",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.akfood.vn/wp-content/uploads/2024/10/anh-dai-dien-1.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("2449f48e-71f4-4ecc-b70e-1fa51c2e107f"),
            Code = "MUNGBEAN_SPROUT",
            Value = "Mungbean Sprout",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.tcmworld.org/wp-content/uploads/2019/03/sprouted-mung-beans.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("c52ce7e5-7470-4f33-b25f-9fc05f5411c2"),
            Code = "GINGER",
            Value = "Ginger",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn2.tuoitre.vn/thumb_w/1200/471584752817336320/2024/10/18/gung-gay-ra-cac-tuong-tac-thuoc-nao-17151219720941211477045-0-0-785-1500-crop-1729239548947663726498.jpeg"
        },
        new Tag
        {
            Id = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a"),
            Code = "GREEN_ONION",
            Value = "Green Onion",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cayantraidetrong.com/wp-content/uploads/2021/08/cay-hanh-la-2.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d"),
            Code = "SHALLOT",
            Value = "Shallot",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.pinimg.com/736x/70/e5/ab/70e5abf381eba4398a00b1552679a49e.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f"),
            Code = "BITTER_MELON",
            Value = "Bitter Melon",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://lh5.googleusercontent.com/fQpcsFpQaR6Ac-KzEMbgweFoZbpPCna8TE9SxERmUN5Q8HDyCgKFnN5lGPnIjT7H89yKwLj1Kojw2ACRlGhBAW5tfOwnd1LmQN-n3rK24kIFoaFYMF422_sZ8MzAAmDPYK-kYl0-dEDznY51eZoEeANx3zgMNVLhkb9fLDE4-5_8eCUN5w-Ohukmvg"
        },
        new Tag
        {
            Id = Guid.Parse("8fba2203-72cb-4870-a4f1-1189f804100b"),
            Code = "TARO",
            Value = "Taro",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://images.baodantoc.vn/uploads/2024/Thang-11/Ngay-1/My-Thanh/thanh-phan-dinh-duong-khoai-mon-1024x576.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("704fb7c2-bd4c-426e-9cec-f86711385e36"),
            Code = "POTATO",
            Value = "Potato",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.vinmec.com/static/uploads/20200418_145937_369667_khoai_tay_max_1800x1800_jpg_a13bcdfa70.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("9c31dc1f-d9d5-4e71-90ae-23b5c666a90b"),
            Code = "KIWI",
            Value = "Kiwi",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://png.pngtree.com/thumb_back/fh260/background/20210912/pngtree-fresh-fruit-kiwi-fruit-image_863906.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79"),
            Code = "BAMBOO_SHOOT",
            Value = "Bamboo Shoot",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://media.istockphoto.com/id/1319370074/photo/freshly-picked-bamboo-shoots-on-wood-background.jpg?s=612x612&w=0&k=20&c=YC3nJ8LprMgyJgpWj3HYyfSNN-p3rH3xHAVbZtQzY5M="
        },
        new Tag
        {
            Id = Guid.Parse("a5b81db6-56bf-4a07-876b-4b4286b46f8d"),
            Code = "LUFFA",
            Value = "Luffa",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://hongngochospital.vn/wp-content/uploads/2013/11/m%C6%B0%C6%A1p-2.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("2212127c-069e-4ac5-97e5-7fca16819c7a"),
            Code = "KING_TRUMPET_MUSHROOM",
            Value = "King Trumpet Mushroom",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.dartagnan.com/dw/image/v2/BJQL_PRD/on/demandware.static/-/Sites-dartagnan-live-catalog-en/default/dwb2201f14/images/products/MFTRU004-1.jpg?sw=635&strip=false"
        },
        new Tag
        {
            Id = Guid.Parse("e61a8226-4c90-4f96-9b5d-b80268345623"),
            Code = "SHIITAKE_MUSHROOMS",
            Value = "Shiitake Mushrooms",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://www.dartagnan.com/dw/image/v2/BJQL_PRD/on/demandware.static/-/Sites-dartagnan-live-catalog-en/default/dwa67d24ad/images/products/MUSCUL009-1.jpg?sw=635&strip=false"
        },
        new Tag
        {
            Id = Guid.Parse("e0c01336-f339-46c3-903b-89fb1b6b5505"),
            Code = "ENOKI_MUSHROOM",
            Value = "Enoki Mushroom",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://yummyaddiction.com/wp-content/uploads/2023/11/how-to-cook-enoki-mushrooms-featured.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("2fa8f0fa-87d3-4ed0-8034-8a0fcb43e718"),
            Code = "BLACK_FUNGUS",
            Value = "Black Fungus",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.pinimg.com/736x/1e/1c/9f/1e1c9f2aec79a975b3d0b40ccac2a972.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("c00c8daa-870b-41d3-90a8-a706e34c3096"),
            Code = "FLOWER_SNAILS",
            Value = "Flower Snails",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i.pinimg.com/736x/f3/7d/52/f37d528c81787d495b7b7d7f42e95eb3.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("02131c94-db6a-470c-ad4c-507b1ff1c6aa"),
            Code = "BELL_PEPPER",
            Value = "Bell Pepper",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://as1.ftcdn.net/v2/jpg/01/39/25/42/1000_F_139254272_RDIyHcAR39zqjsbikYsmTsZMgw65AjLR.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("3f5093c0-b059-4210-8a4f-bb92f80500d0"),
            Code = "LEMONGRASS",
            Value = "Lemongrass",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://ecoairvietnam.com/wp-content/uploads/2023/11/chuot-co-so-mui-sa-khong-1.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc"),
            Code = "PORK_RIBS",
            Value = "Pork Ribs",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://media.istockphoto.com/id/1465370619/photo/raw-pork-ribs-with-rosemary-and-spices-on-rustic-wooden-cutting-board-prepared-for-cooking-on.jpg?s=612x612&w=0&k=20&c=ci7XIGrx9adaJ7ebdpo5ZtF-APKxN3mYacOH5PPFxLA="
        },
        new Tag
        {
            Id = Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820"),
            Code = "GROUND_PORK",
            Value = "Ground Pork",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://img.freepik.com/free-photo/fresh-minced-meat-ready-cooking_1220-4988.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("0e24b473-9d14-40a5-b239-0666c6c0a920"),
            Code = "CHICKEN_HEART",
            Value = "Chicken Heart",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://cdn11.bigcommerce.com/s-a21u9f1c98/product_images/uploaded_images/chicken-heart-yakitori.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece"),
            Code = "SHRIMP",
            Value = "Shrimp",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://static.vecteezy.com/system/resources/previews/025/270/054/non_2x/shrimp-raw-gambas-fresh-seafood-prawn-meal-food-snack-on-the-table-copy-space-food-background-rustic-top-view-photo.jpg"
        },
        new Tag
        {
            Id = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa"),
            Code = "LETTUCE",
            Value = "Lettuce",
            Category = TagCategory.Ingredient,
            Status = TagStatus.Active,
            ImageUrl = "https://i0.wp.com/post.healthline.com/wp-content/uploads/2020/03/romaine-lettuce-1296x728-body.jpg?w=1155&h=1528"
        },
        //Category tag
        new Tag
        {
            Id = Guid.Parse("71676633-493e-46c5-86a0-21773f196035"),
            Code = "ALL",
            Value = "All",
            Category = TagCategory.DishType,
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/hivalxshuvx5nnlfp5t1.png",
        },
        new Tag
        {
            Id = Guid.Parse("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
            Code = "NOODLES",
            Value = "Noodles",
            Category = TagCategory.DishType,
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196327/default_storage/tag/dishtype/ingre9ficyzq0iitiwmd.png",
        },
        new Tag
        {
            Id = Guid.Parse("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
            Code = "SPICE",
            Value = "Spice",
            Category = TagCategory.DishType,
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196323/default_storage/tag/dishtype/frgtox8kb4edsvmutmez.png",
        },
        new Tag
        {
            Id = Guid.Parse("d8b74fc2-f848-41af-a53f-20170aa453cd"),
            Code = "BBQ",
            Value = "BBQ",
            Category = TagCategory.DishType,
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196328/default_storage/tag/dishtype/zl1f9g0dxxbtw2f4jiel.png",
        },
        new Tag
        {
            Id = Guid.Parse("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
            Code = "SEAFOOD",
            Value = "Seafood",
            Category = TagCategory.DishType,
            Status = TagStatus.Active,
            ImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735196329/default_storage/tag/dishtype/jhxdroetbjq9f57cixwj.png",
        },

    ];

}
