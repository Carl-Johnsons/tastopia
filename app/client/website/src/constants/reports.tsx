import { RecipeReport } from "@/types/report";
import { faker } from "@faker-js/faker";
import { IRecipe } from "../../../mobile/generated/interfaces/recipe.interface";
import { IUser } from "../../../mobile/generated/interfaces/user.interface";

faker.seed(0);

const imageUrls = [
  "https://www.blessthismessplease.com/wp-content/uploads/2020/04/green-smoothie-recipe-1-of-15.jpg",
  "https://www.dishgen.com/_next/image?url=%2Fimages%2Fplaceholder%2Fplaceholder-comfort.jpg&w=828&q=75",
  "https://thewoksoflife.com/wp-content/uploads/2015/10/ma-la-xiang-guo-13.jpg",
  "https://www.allrecipes.com/thmb/8EO9lgHH-dObc75Oj3GZI_qRd5E=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/ALR-79793-grandma-leachs-fruitcake-VAT-4x3-greenbackground-1-2cb509b5bd8c4a148061404bab13c00d.jpg",
  "https://creator.nightcafe.studio/jobs/Mw0RMsPIk4OJCIGas8Jp/Mw0RMsPIk4OJCIGas8Jp--1--BRYM4_2x.jpg",
  "https://content.instructables.com/FDI/L118/I24MA1SM/FDIL118I24MA1SM.jpg?auto=webp&fit=bounds&frame=1&height=1024&width=1024auto=webp&frame=1&height=150",
  "https://i0.wp.com/www.onehundreddollarsamonth.com/wp-content/uploads/2017/10/alien-body-with-drinks.jpg?resize=600%2C450&ssl=1",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ0Kspi4-uhHfNCt_TES1qJvlRtGCwT8oZegA&s",
  "https://i0.wp.com/smittenkitchen.com/wp-content/uploads//2014/10/green-bean-casserole-with-crispy-onions1.jpg?fit=640%2C427&ssl=1",
  "https://shopbearhollow.com/cdn/shop/products/IMG_3120_800x.jpg?v=1605551469",
  "https://www.coconutandlime.com/wp-content/uploads/2007/01/saucemeatloaf.jpg",
  "https://i.ytimg.com/vi/Lc1rscW-JMk/hqdefault.jpg",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXLwzByCllGKXqqEcxZS3KOiBOwKyy1O-zEg&s",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT2_YucwGBUpb5OmAawSwf_sb2LxZtoSWEtDg&s",
  "https://i.ytimg.com/vi/TexEMHehkFE/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLD4qOSl3u9r0DmcvNQwAAYXR76geg",
  "https://www.thespicehouse.com/cdn/shop/articles/Pickled_Pigs_Feet_720x.jpg?v=1588186226",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_gLQf10WdmFFmsEafMimbzBUeWgqJnwvaow&s",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9FkRlmZdP-LMb5H7PTfWd0V_9WqMKu5Goog&s",
  "https://historicalitaliancooking.home.blog/wp-content/uploads/2020/05/ova-spongia-piatto.jpg",
  "https://s.abcnews.com/images/GMA/HT_FRENCHS_PINT_SAND_BOWL_hpMain_16x9_1600.jpg",
  "https://www.closetcooking.com/wp-content/uploads/2010/06/Ahi-Tuna-Tostadas-with-Wasabi-Lime-Aioli-500.jpg",
  "https://i0.wp.com/seonkyounglongest.com/wp-content/uploads/2017/03/IMG_0123.jpg?resize=1000%2C562",
  "https://i.etsystatic.com/8713651/r/il/eeae4b/3473578580/il_570xN.3473578580_g45g.jpg",
  "https://healthynibblesandbits.com/wp-content/uploads/2017/05/Strawberry-Tofu-Spring-Rolls-FF-500x375.jpg",
  "https://www.wokandkin.com/wp-content/uploads/2020/10/Braised-Honeycomb-Tripe-saved-for-web-500x375.png",
  "https://brianfink.wordpress.com/wp-content/uploads/2012/06/liver-lima-bean-stew-01.jpg",
  "https://m.media-amazon.com/images/I/81BfvxkUH-L._AC_UF350,350_QL80_.jpg",
  "https://grillinfools.com/wp-content/uploads/2023/11/BBQ-Pizza-with-Pickles-20-scaled.jpg",
  "https://irepo.primecp.com/2016/08/293692/Creamy-Corn-Broccoli-Bake_ExtraLarge1000_ID-1802524.jpg?v=1802524",
  "https://media.tegna-media.com/assets/WNEP/images/b782b36b-615d-41e8-a958-d31e22c4c7e5/b782b36b-615d-41e8-a958-d31e22c4c7e5_1140x641.jpg",
  "https://dishnthekitchen.com/wp-content/uploads/2022/12/squareambrosiasalad-720x540.jpg",
  "https://images.squarespace-cdn.com/content/v1/55784d01e4b0b6a84d2ad45c/1555892894386-ONMUPUWO8O8FVPNR3GZT/Choc+Pudding+Parfait+16x9.jpg",
  "https://assets.bonappetit.com/photos/65b814766190271c9c847c20/4:3/pass/undefined",
  "https://img.freepik.com/premium-photo/thai-cotton-candy-burrito-pancake-roti-saimai-wooden-plate_484521-2211.jpg",
  "https://www.margaretrivermail.com.au/images/transform/v1/crop/frm/wXRNchq95bZhpeysFncAhm/783e6aae-f009-41f8-9605-6f6f7ea80168.png/r0_9_1367_778_w1200_h678_fmax.jpg",
  "https://lirp.cdn-website.com/md/pexels/dms3rep/multi/opt/pexels-photo-3957501-640w.jpeg",
  "https://www.favfamilyrecipes.com/wp-content/uploads/2025/01/Toothpick-Grape-Jelly-Meatball.jpg",
  "https://www.simplyrecipes.com/thmb/TYg5aw7i5ewk2gQ_CuvIBqCCRXE=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Simply-Recipes-Andrew-Zimm-Sandwich-LEAD-04-e526f59e8eec4327917f2291d2a510fe.jpg",
  "https://storage.googleapis.com/dam-prs-prd-c7e7986.prs.prd.v8.commerce.mi9cloud.com/1%20Recipes/F23_Prego_Chunky_Tomato_Three_Cheese_Cheesy_Tortellini_Bake_600x360.jpg",
  "https://cascadeglacier.com/wp-content/uploads/2016/12/BubbleGum.jpg",
  "https://sm.mashable.com/mashable_in/seo/2/25824/25824_jzqs.jpg",
  "https://lh6.ggpht.com/-kU_k9G32V30/UKBQBqtvaMI/AAAAAAAACeg/0dEEwvwgvOg/Tacos-de-tripita4_thumb2.jpg?imgmax=800",
  "https://rockinmama.net/wp-content/uploads/2018/10/Flamin-Hot-Wings-630x426.png",
  "https://www.tastingtable.com/img/gallery/philadelphias-chocolate-covered-onion-started-as-a-punchline/l-intro-1678389049.jpg",
  "https://www.hot-dinners.com/images/stories/blog/2024/frantzenwaffles2.jpg",
  "https://wicklespickles.com/wp-content/uploads/2018/07/Web_Recipe_Popsicle_2.jpg",
  "https://www.houseofcaviarandfinefoods.com/wp-content/uploads/2016/12/holiday-recipe-ideas.jpg",
  "https://homemadewithmess.wordpress.com/wp-content/uploads/2016/01/thumb_img_7987_1024.jpg",
  "https://www.confessionsofachocoholic.com/wp-content/uploads/2016/06/avocado-ice-cream.jpg",
  "https://www.eatingwell.com/thmb/amEGT1QdlqXSxVS3UR2ynk6vmU8=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/How-Long-Do-Brussels-Sprouts-Last-2ce35364587c4b5491360082ee8ba3f2.jpg",
  "https://irepo.primecp.com/2015/07/227197/Cherry-Cola-Salad_ExtraLarge1000_ID-1076409.jpg?v=1076409",
  "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEjpaJEslMej92UUmUItJaIIwgZrOQLK_BYO_WN0eYNWO5dL3Q3GscYGrfWyEyrvRR3cUk_FBoJq6antpZ7JAcQ8vtRZUQzPiiWafpj4Lbr4TIgAY1ijBuHoKjPhe2DkPlJcwHDNWKLr8fo/w1200-h630-p-k-no-nu/IMG_0761.jpg",
  "https://thethreebiterule.com/wp-content/uploads/2021/09/hot_dog_pizza_over_590_390.jpg",
  "https://images.getrecipekit.com/20230612210154-1600x1000.webp?class=16x9",
  "https://www.snapcalorie.com/blog/content/images/2023/08/can-dogs-eat-sauerkraut.webp",
  "https://cdn.shopify.com/s/files/1/0603/5439/6408/files/20240617212652-1600x1000.webp?v=1718659614",
  "https://www.allrecipes.com/thmb/gswTdPR34qdII21cR1EE1M1uHds=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/AR-eggs-on-pizza-4x3-7caa5af2642e4039abedc3ba54272d7e.jpg",
  "https://sp-ao.shortpixel.ai/client/q_glossy,ret_img,w_640,h_427/http://noteatingoutinny.com/wp-content/uploads/45783624142_5a88024f5e_z.jpg",
  "https://recipeland.com/rails/active_storage/representations/proxy/eyJfcmFpbHMiOnsiZGF0YSI6MTM5NzcsInB1ciI6ImJsb2JfaWQifX0=--b5b194f73e0beaa7acba2277d869be4285a65469/eyJfcmFpbHMiOnsiZGF0YSI6eyJmb3JtYXQiOiJqcGciLCJyZXNpemVfdG9fbGltaXQiOls4NjAsbnVsbF0sImNvbnZlcnQiOiJ3ZWJwIiwic2F2ZXIiOnsic3Vic2FtcGxlX21vZGUiOiJvbiIsInN0cmlwIjpmYWxzZSwiaW50ZXJsYWNlIjp0cnVlLCJxdWFsaXR5Ijo1MH19LCJwdXIiOiJ2YXJpYXRpb24ifX0=--4288720e597eeb129da100809b95192d1d38cc9d/orig_028705a870e07a382695.jpg",
  "https://bastecutfold.com/wp-content/uploads/2024/04/DSC_0136-6-e1740090507404.jpg",
  "https://i2-prod.mirror.co.uk/incoming/article34752617.ece/ALTERNATES/s1200d/0_PinPep_GoldenSyrup_DL_001.jpg",
  "https://tastykitchen.com/recipes/wp-content/uploads/sites/2/2010/02/pizzaB1.jpg",
  "https://files.cleanfooddirtygirl.com/20230808143100/whole-food-plant-based-mango-cowgirl-caviar.jpg",
  "https://wineandplum.com/wp-content/uploads/2011/05/dscn1167.jpg",
  "https://eatingrichly.com/wp-content/uploads/2012/04/spam-musubi-recipe-fb.jpg",
  "https://i0.wp.com/smittenkitchen.com/wp-content/uploads/2021/07/deviled-eggs-scaled.jpg?fit=750%2C500&ssl=1",
  "https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEg-RMZo370M_0put28j7lpQc5ldM2Iq3rJe4oXWDrgaUR1f8PnBGLFauYNLWgWYKugoYr-M8dMHJJZ7K7OSVC5UGViDEwEw6AdWKPVdMml_0-IpJ9mDcOyIhEhzfnFiaVe5Hk11T-bkeWE/w1200-h630-p-k-no-nu/Rhode+Island+Clam+Chowder_4203+words.jpg",
  "https://files.mob-cdn.co.uk/recipes/2023/09/_800x418_crop_center-center_82_none/Kimchi-Peanut-Butter-Toast.jpg?mtime=1694096406",
  "https://www.verybestbaking.com/sites/g/files/jgfbjl326/files/styles/facebook_share/public/recipe-thumbnail/96232-2020_04_29T15_03_47_mrs_ImageRecipes_145497lrg.jpg?itok=AVpWC9o9"
];

function getRandomRecipeImage() {
  return faker.helpers.arrayElement(imageUrls);
}

export const recipeData: RecipeReport[] = Array.from({ length: 5 }, (_, id) => ({
  id: id + 1,
  recipeName: faker.commerce.productName(),
  recipeOwner: faker.internet.username(),
  recipeImageUrl: getRandomRecipeImage(),
  reporter: faker.internet.displayName(),
  reporterAvatar: faker.image.avatar(),
  reportReason: faker.lorem.words(3),
  reportCodes: Array.from({ length: 3 }, () => faker.lorem.words(1).toUpperCase()),
  createdDate: faker.date.past().toISOString().split("T")[0],
  status: faker.helpers.arrayElement(["PENDING", "DONE"])
}));

export const randomRecipe: IRecipe = {
  authorId: faker.string.uuid(),
  title: faker.commerce.productName(),
  description: faker.lorem.paragraph(),
  imageUrl: getRandomRecipeImage(),
  ingredients: Array.from({ length: faker.number.int({ min: 3, max: 10 }) }, () =>
    faker.commerce.product()
  ),
  cookTime: `${faker.number.int({ min: 10, max: 120 })} min`,
  serves: faker.number.int({ min: 1, max: 8 }),
  voteDiff: faker.number.int({ min: -10, max: 50 }),
  numberOfComment: faker.number.int({ min: 0, max: 5 }),
  isActive: faker.datatype.boolean(),
  totalView: faker.number.int({ min: 100, max: 10000 }),
  steps: Array.from({ length: faker.number.int({ min: 2, max: 6 }) }, (_, index) => ({
    ordinalNumber: index + 1,
    content: faker.lorem.sentence(),
    attachedImageUrls: [getRandomRecipeImage()],
    createdAt: faker.date.past().toISOString(),
    updatedAt: faker.date.recent().toISOString(),
    id: faker.string.uuid()
  })),
  comments: Array.from({ length: faker.number.int({ min: 0, max: 5 }) }, () => ({
    content: faker.lorem.sentence(),
    accountId: faker.string.uuid(),
    isActive: faker.datatype.boolean(),
    createdAt: faker.date.past().toISOString(),
    updatedAt: faker.date.recent().toISOString(),
    id: faker.string.uuid()
  })),
  recipeVotes: Array.from({ length: faker.number.int({ min: 0, max: 20 }) }, () => ({
    accountId: faker.string.uuid(),
    isUpvote: faker.datatype.boolean(),
    createdAt: faker.date.past().toISOString(),
    updatedAt: faker.date.recent().toISOString(),
    id: faker.string.uuid()
  })),
  recipeTags: Array.from({ length: faker.number.int({ min: 1, max: 3 }) }, () => ({
    recipeId: faker.string.uuid(),
    tagId: faker.string.uuid(),
    id: faker.string.uuid()
  })),
  createdAt: faker.date.past().toISOString(),
  updatedAt: faker.date.recent().toISOString(),
  id: faker.string.uuid()
};

export const randomUser: IUser = {
  accountId: faker.string.uuid(),
  displayName: faker.person.fullName(),
  avatarUrl: faker.image.avatar(),
  backgroundUrl: faker.image.urlPicsumPhotos(),
  dob: faker.date
    .birthdate({ min: 18, max: 65, mode: "age" })
    .toISOString()
    .split("T")[0],
  gender: faker.helpers.arrayElement(["Male", "Female"]),
  bio: faker.person.bio(),
  address: faker.location.city() + ", " + faker.location.country(),
  totalFollower: faker.number.int({ min: 0, max: 10000 }),
  totalFollowing: faker.number.int({ min: 0, max: 5000 }),
  totalRecipe: faker.number.int({ min: 0, max: 100 }),
  isAccountActive: faker.datatype.boolean(),
  accountUsername: faker.internet.username(),
  isAdmin: false
};

console.log(randomUser);
