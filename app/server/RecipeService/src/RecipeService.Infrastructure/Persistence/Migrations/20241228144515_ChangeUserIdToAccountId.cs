using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdToAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("002373cd-c321-4326-bcee-f1376c88e238"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("036e4c18-e064-44ea-a4b1-ccbf7b236a07"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("04508e64-caab-46bc-9f98-1fc9d0cbabd1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0679e33c-43c8-4446-b86a-82a7a38724f2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0cbc0088-8ef1-48b9-83d7-55b6c98f9a87"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0d4fc64c-21c8-4521-a6a4-9f3d47db1dc4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0f2996e8-b4e9-4761-b9f9-f58b58fb4b83"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0f928b79-1146-4e3c-b1cf-542577f02ec7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("124e8a5b-d758-404a-8e3e-9aebcf0d389b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("13a22e3e-fda1-4cd4-8a0f-742d359006a2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1437c5b2-6bbd-4cf8-88b9-71bb01f6f5f9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("144884dd-35d7-4112-ace6-0e7141f01e90"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("160404ba-599f-40f8-afe4-dd28c583809f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1fda2a0e-4478-4d11-8802-864e922b9b59"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("244f1c9e-5fb2-4173-a1ee-65a7bddf6070"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("24c65916-5483-4ce2-8aef-aa8bdc66ddfb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("26618d5f-ea76-477c-a342-9086ed61afe0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("280329aa-2a02-4d4a-9681-6df8c6f6632e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2dff8b9c-a697-4c87-8a1c-1451d8296e8d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2ee6794b-dae6-4f99-a09f-3f8593ee7cfd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("330adf86-852a-436f-bce8-676595dd2b42"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("34cf5a22-c76d-4521-a3d9-55fa041bca3d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("34e86fae-f742-4e0d-a500-b5e91b15a19e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("35d9f992-361a-43ef-b717-00ccd7f5e47b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3d797d53-c2fa-49ab-9a4a-595ac606e686"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3e704a49-bed5-4d87-a5e7-a820858fb699"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("40fa8bca-5273-48fe-84c1-151f08b9e20c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("44711740-1ca2-4e4d-ab62-07fa41f0d7c7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4ab77ae6-8575-43a3-b960-a63beba88cdc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4ae36fed-03a4-453f-ba63-bd55d1388b58"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4be7f0bc-1c02-4a6c-aaec-c545c311d9d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4e534748-482b-48ae-a1f3-223f4087ccad"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4e67eed5-6b75-4b4d-b78f-b03276459eef"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5099ea31-7c0c-430e-a7e3-3a877368dac1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("50fc3c23-1469-498b-b50d-e70892d1d290"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("577f43f5-0096-4490-a8a9-ecb5c65b2ed9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5baa9ad7-5a40-421d-9426-1cb6cc0e536d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("61040e4a-f40d-430c-b201-5bca6f4b64f4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("61711833-09a9-4bfd-bb97-5759ff04b50f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("634b927e-1539-419a-b33b-c9397bfa0aee"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6410811f-6e12-4cab-b278-4529428d1fb6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("65308f7a-bfa9-4161-8629-829b3160e772"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("65f0d43f-ac24-4e51-8486-4207fa2f1f28"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6c31d210-fce3-4ab0-897b-dfafc5c30271"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6dde54cc-4252-462b-b5f0-375a4743cae8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("71c7eaef-b7e1-45c2-b41e-36dc14af08ef"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("754ce26b-dc93-42f3-8636-34ad860fe730"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("75847d5f-b119-48bb-afed-157cc16d377b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("797e0938-66be-45bd-baca-b02b1375959a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7b244fb8-8c43-4a4f-9764-5e2bbb181177"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7b726fcc-63c4-4c33-ad94-a3d768be89d3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7c768cf5-b351-4341-a11b-f4f47f0f3f1c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7c86e6e3-c9f8-4fd1-b00a-ca4e85f02ac9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7d4c3896-3f75-4082-bab6-ef9a02fbb673"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("80c622f6-c66a-490a-9eb5-32dadffbccd3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("814b37a2-ea2d-4f8f-92aa-2f26f939f356"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("820e1fc7-c5ce-48ff-9fbb-8eb77caa95d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("823b58eb-57df-4433-9d8e-45a910636e45"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8792cf3f-973e-4fde-ae06-204461d2225d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8a31fa0f-a3eb-4f68-a7f0-f67195ff9cc8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9416ff68-2394-44a3-a7a4-708f0b146eee"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("96786985-5ea6-4047-acc2-9b26e492ae9e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("98fde936-8738-46ae-b145-bd67692126c3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9fa4a5d6-0bc0-47fe-b408-1c05977dffe7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9fd7f611-7c49-4ad8-a666-9a37293ca824"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9fd7f7b7-4998-4617-96f3-f48c69e4476c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a0a168ad-ccd8-4ff6-8cb6-ab24b75d2c87"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a2523826-e1d7-4bde-abe2-ac33dfea5475"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a3ed4267-5379-4dd0-be2b-8a44e8fa3946"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a88a99b4-cb4c-4890-96df-3825776b3be7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("aa7cf4cb-274f-465e-ae49-246f9cfb78fe"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ab49ec4f-0b21-403f-b833-835331f23ac1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ad637a25-7ccf-424a-bc25-232962c48fbc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("aee49ef5-0b2b-4112-bfb2-052a9319a78b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b0a330f5-d837-45a2-9580-fab8e929292e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b0d08ac6-0a05-483f-96f1-3449d0c91aa6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b3522cd1-49ef-4536-b1a7-a562010a7235"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b560505c-a826-4bf9-ac80-00c706a3ff1c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b6ccbb8d-3443-4f11-b94d-75f986a91521"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b8f4c726-c5b5-4eed-afe9-dc5bf8941587"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("be18a840-49ef-44a1-89a3-8afde331a982"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c0dcf25c-23f9-4561-bb2d-c7112ac4ead4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c396fdab-3053-4510-933e-7c7e7d687f32"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cacf7182-f273-4ef3-88ca-36872ddde97c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("caef8804-734d-4271-a4d1-9b4d54bd282d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cb1891dd-e82c-4044-99bc-3125b9a07285"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cb8d0f57-f677-4cdd-b834-63f6989ecf3b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cdbd4769-a7d4-4309-9a41-11a4b36df8a5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d11c1ee9-7860-40ce-b29a-6a564da8b58f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d4e50317-fad9-491f-beb4-0976985eaad6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d5d5178c-435c-4e73-a0a4-617756c0a433"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d9bc34d7-8570-48c1-a54d-39dd0d9ddd39"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e23221b0-9f0f-45b2-958c-0ed5b902695e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e2baf9c3-3d8d-4d0d-900f-8a575a2e1968"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e2c1c808-a1c9-437d-856e-2e9dbc2a4e1c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ed3787c0-80a7-414d-bc20-9377037283b4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f56e0f81-b057-4fa2-8eca-6ec89e9d64cb"));

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReportRecipe",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserReportComment",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserBookmarkRecipe",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RecipeVote",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CommentVote",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comment",
                newName: "AccountId");

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                column: "Ingredients",
                value: new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                column: "Ingredients",
                value: new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"),
                column: "Ingredients",
                value: new List<string> { "4 Carrots", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
                column: "Ingredients",
                value: new List<string> { "1 Broccoli Head", "200g Cheese", "1 Onion", "2 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
                column: "Ingredients",
                value: new List<string> { "2 cups Milk", "100g Ice Cream", "1 tbsp Sugar" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
                column: "Ingredients",
                value: new List<string> { "4 Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Basil", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
                column: "Ingredients",
                value: new List<string> { "200g Shrimp", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper", "Parsley" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
                column: "Ingredients",
                value: new List<string> { "1 cup Rice", "2 Eggs", "1 Carrot", "Soy Sauce", "2 tbsp Olive Oil", "Garlic" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "50g Cheese", "Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
                column: "Ingredients",
                value: new List<string> { "1 cup Rice", "1 Onion", "2 Garlic Cloves", "1 Carrot", "1 tbsp Soy Sauce", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "200g Minced Beef", "1 Onion", "2 Garlic Cloves", "4 Tomatoes", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                column: "Ingredients",
                value: new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"),
                column: "Ingredients",
                value: new List<string> { "200g Mushrooms", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("d2189f90-6991-4901-8195-f0c12d24d900"),
                column: "Ingredients",
                value: new List<string> { "4 Chicken Breasts", "BBQ Sauce", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "100g Bacon", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
                column: "Ingredients",
                value: new List<string> { "200g Mushrooms", "100g Cheese", "2 Garlic Cloves", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
                column: "Ingredients",
                value: new List<string> { "4 Potatoes", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
                column: "Ingredients",
                value: new List<string> { "1 Carrot", "1 Broccoli Head", "Soy Sauce", "Olive Oil", "Salt", "Pepper" });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedImageUrls", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0204be9f-51a2-4657-86ce-99ce96210a99"), null, "Cook the spaghetti according to package instructions and drain.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04a8abe0-a83f-4f1e-b2a0-d697b5b833c8"), null, "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0e76726e-2518-4de3-9187-80fb702c86f9"), null, "Peel and chop the potatoes into chunks.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10e7faf6-8936-415c-b8e8-9a536a5d1ef2"), null, "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1221f9d2-0047-4282-9800-df9d61e45dfb"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1319e595-95ec-426f-82f3-e3d1055e1365"), null, "Add chopped carrots and garlic to the pan, and cook until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("13a06b09-d639-444d-9563-8a4183694590"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("14252c2d-c2bf-4b21-9abb-f00f29e4bf41"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("17625349-cf5d-45eb-a363-9fd8c6c0af7f"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1ff24cee-5b3a-4a05-9402-27e7305608bc"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("20a467a9-9c9a-4920-b4b3-f308439aa163"), null, "Drain the cooked spaghetti and toss with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("240e594b-d309-4e0d-8ceb-04a2859d3cc8"), null, "Season with salt, pepper, and parsley before serving.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2610e701-7e58-4d9e-be86-0070e13b5b31"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("28488ca1-c0e3-4716-9df2-d293d45c5b0b"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2aecf86e-c6f5-46fa-bd9e-929347923240"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2bb52b6e-5538-43ca-9876-ea257390e629"), null, "In a bowl, whisk together the eggs and Parmesan cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("319c34d7-eb97-4df1-9419-1fe95fadf0fc"), null, "Roast the garlic cloves in the oven for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("35d958cf-1fd3-4aa0-9918-d7c3b0840a34"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("35eda4de-d807-4991-9c47-720a93626223"), null, "Sauté the chopped mushroom stems and garlic in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("36a915cb-bf8f-48cd-b62b-0dbb165c291b"), null, "Serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("39d0faae-b955-4fc8-9dfb-2de54e765fbc"), null, "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44571fc7-ee20-4624-a51f-a005e4bc61d5"), null, "Season with salt and pepper to taste, and serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("48406ef2-d95b-4844-a168-22382567e63c"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4fe1def3-b77b-470f-9f63-811154d362b5"), null, "Crack the eggs into the pan and cook to your desired doneness.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("525d6d3d-3336-4131-b297-c5f1629a6ecd"), null, "Season with salt and pepper, then blend the soup until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("59b96c52-02e9-48da-91d6-9682922f7fed"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5b1d8b19-d575-43d8-813d-89fe93750bfc"), null, "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5f28f055-6913-46b2-abaf-5d9cca50e1ce"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6f69cd43-ad9e-4009-acbe-b3bc38eed4a4"), null, "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6f98168c-4c81-4d33-b0a5-a6e55dc61184"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("722a2415-e3be-43bd-bd17-f7bc2a118fb1"), null, "Cook the minced beef in a pan until browned.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7323ae81-cdd2-41f3-a219-96badbb3ca7e"), null, "Boil the potatoes in salted water until tender, about 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("757d55d4-ee19-49f3-ac13-429350df1bb6"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("75b398b0-81c3-44e0-94df-e9757564f642"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("764def37-c227-4928-9ec4-bacbf01a5909"), null, "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("78ab49eb-d509-4708-9343-626185e5f895"), null, "Season the chicken breasts with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7ebcf384-be57-43b5-8a72-6d92c48c7d44"), null, "Cook rice according to package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7ff5ff68-87ae-4e63-8d9b-e6a7f0af9204"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("81922d82-24f8-4b7a-84ee-7f878968738c"), null, "Pour the egg mixture into the pan and cook until set, gently folding the edges.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("85bb1391-f3e9-4f0c-93b5-1ad43b37f154"), null, "Add the carrots and stir-fry for 3-4 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("86f27aa3-eefc-4006-aae3-7534f07754f2"), null, "Fry the bacon in a pan until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("87ec98a6-34c8-4f2b-9b59-3a3bbf1af80d"), null, "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8848b057-0334-4084-aef3-7c97a1b805ab"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8911b285-febd-4c57-9525-a53e222c0c9d"), null, "Melt butter in a pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a0b88c8-0482-4c45-86f5-bf6d8b6a6b98"), null, "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8b843c24-812b-45e1-aa77-b48016404945"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8d1d27bc-c9b1-4ad1-9436-1ece7dd79ef2"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8ec5ba37-ecbd-4e14-9363-e61a4de9ff95"), null, "Season with salt and pepper to taste.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("913d7fc0-1d74-4177-a4f2-c0c82bcc7355"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("924eb182-50b7-43c1-82ae-bbd6539986f0"), null, "Cook for 20 minutes, or until rice is tender and water is absorbed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("92ff87ba-b256-4644-b052-fd3bb576d1b8"), null, "Add the chopped mushrooms and cook until they release their juices.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("96dc8279-7d7b-416d-b56d-51015775c7ab"), null, "Season with soy sauce, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9eb43d6e-cd57-45e3-a515-654b13249ac4"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9f1df930-2a52-4607-b60c-d295ffd10071"), null, "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2404fcc-2f3c-4941-90ca-ca403f1be0d2"), null, "Grill the chicken for 6-7 minutes on each side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2fc334d-fe58-4703-a810-f1bf966ab061"), null, "Scramble the eggs in a pan with some oil or butter.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a9ff8555-0a99-43fa-9aa0-a9d8f4683fcd"), null, "Cook the spaghetti according to the package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aa0245fe-d16c-4503-bc9a-233a5207e38f"), null, "Chop the onion, garlic, and tomatoes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aa266b72-d001-4c54-9517-072b39592428"), null, "Fry the bacon in a pan until crispy, then remove and chop.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ac861c10-caf9-4c59-849d-e6de7e9adda6"), null, "Steam the broccoli until tender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("af7b5571-90b1-4771-b538-68eaa8440475"), null, "Chop the mushrooms, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b0e04240-e971-4a90-a11c-013bb6f30637"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b108df5d-3157-4873-b83f-f6ffb2cf6d41"), null, "Add the shrimp to the pan and cook until pink and opaque.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b2eef28b-405d-4970-8687-53c8bb1bf450"), null, "Sauté the onions and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b5269a11-01ee-4304-b5bf-714ec56d27d8"), null, "Blend the soup until smooth, then serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b84c1c85-47cb-412b-8555-9d75cd489585"), null, "Preheat the grill to medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("baa85653-eab0-45b4-852b-f2e55fa633b5"), null, "Peel and devein the shrimp.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bb407d20-42b1-41c8-a3e4-b98af92f6994"), null, "Season with salt, pepper, and fresh basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bbee573b-e5f2-47ec-abc9-5aaf592c3e5a"), null, "Sauté the onion and garlic in a separate pan until softened.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c0a06261-e8fe-4e2d-8359-6af3825293ad"), null, "Heat the olive oil in a pan over medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c2f3c543-451e-4934-a245-4174351b0ba1"), null, "Season the eggs with salt and pepper, then serve with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c54ad6a0-5609-4ad0-9f4e-ba4d0a0bd9ca"), null, "Blend the soup until smooth and serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c6e70d69-91ed-4ff3-8d5c-afed7522a1a6"), null, "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c8f1f302-e2d4-4576-b324-a9f55e21605e"), null, "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c94c0f34-93a5-429d-80c2-818964493997"), null, "Chop the carrot and broccoli into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c978e9ab-24ab-4485-b5b0-5627d0ae2c66"), null, "Remove the stems from the mushrooms and chop them finely.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cc70e9f5-0fe2-4208-b0e1-16f080562ec1"), null, "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cda48341-3744-4364-b6f2-01ce8161b081"), null, "Mash the potatoes with butter, roasted garlic, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d3156ec6-2a9e-4ec2-9b73-2e74a5b5187a"), null, "Melt butter in a pan and sauté garlic until fragrant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d6331b8a-3ff1-4791-97c1-1c53297e15db"), null, "Peel and chop the carrots, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d7ff8adc-6da6-4863-9b54-d9152b86b520"), null, "Add the cooked rice to the pan and stir-fry with soy sauce.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d83adeb4-e144-41cf-b680-28b6bd1b2971"), null, "Add milk, ice cream, and sugar to a blender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dace5a19-37df-4224-a3b9-2be16f03c514"), null, "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dc9d99bb-6a28-4ff3-b74a-39a273634d91"), null, "Serve with additional BBQ sauce on the side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dea08f11-bd48-4ea2-9aeb-455190a800c5"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e342782f-6a62-4f14-a662-d8f503e1714f"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e39e354f-d6dd-4148-8261-bc98d21acef8"), null, "Sauté the onion in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e579b0f5-2583-463e-b16f-9eaf4c97f37d"), null, "Blend until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e757e4d8-81b5-4ab2-9632-42810c749558"), null, "Chop the onion, garlic, and carrot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ef2a4a97-62ec-4c3f-8a0e-ba636084830a"), null, "Serve the Bolognese sauce over the cooked spaghetti.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f2e884d3-fce5-4168-b0c1-ee7ea50119d1"), null, "Add the broccoli and continue stir-frying for another 5 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f62c7c28-0856-48ba-a211-b3e772243bc5"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f7370ed0-db1b-4ab0-8528-8d524868ce78"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fbc5f504-e106-40da-bdc6-cf8aa6f626cd"), null, "Add the rice and soy sauce, then cook for 1-2 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fdc26186-c983-4f06-a1a3-16b2a7559000"), null, "Pour in the vegetable stock and let the soup simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fecf20b0-9487-4362-827a-6f523227a122"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ffa56e86-8e9f-46de-926a-00a71a50ec7c"), null, "Mix the sautéed mushrooms with cheese and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1089), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1090) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1045), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1046) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1061), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1062) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1075), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1076) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1072), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1073) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1081), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1081) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1058), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1059) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1078), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1079) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1067), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1067) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1048), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1049) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1064), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1065) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1051), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1052) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1070), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1070) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1086), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1087) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1084), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1084) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1031), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1043) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1055), new DateTime(2024, 12, 28, 14, 45, 15, 479, DateTimeKind.Utc).AddTicks(1056) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0204be9f-51a2-4657-86ce-99ce96210a99"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("04a8abe0-a83f-4f1e-b2a0-d697b5b833c8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0e76726e-2518-4de3-9187-80fb702c86f9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("10e7faf6-8936-415c-b8e8-9a536a5d1ef2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1221f9d2-0047-4282-9800-df9d61e45dfb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1319e595-95ec-426f-82f3-e3d1055e1365"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("13a06b09-d639-444d-9563-8a4183694590"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("14252c2d-c2bf-4b21-9abb-f00f29e4bf41"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("17625349-cf5d-45eb-a363-9fd8c6c0af7f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1ff24cee-5b3a-4a05-9402-27e7305608bc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("20a467a9-9c9a-4920-b4b3-f308439aa163"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("240e594b-d309-4e0d-8ceb-04a2859d3cc8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2610e701-7e58-4d9e-be86-0070e13b5b31"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("28488ca1-c0e3-4716-9df2-d293d45c5b0b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2aecf86e-c6f5-46fa-bd9e-929347923240"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2bb52b6e-5538-43ca-9876-ea257390e629"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("319c34d7-eb97-4df1-9419-1fe95fadf0fc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("35d958cf-1fd3-4aa0-9918-d7c3b0840a34"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("35eda4de-d807-4991-9c47-720a93626223"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("36a915cb-bf8f-48cd-b62b-0dbb165c291b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("39d0faae-b955-4fc8-9dfb-2de54e765fbc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("44571fc7-ee20-4624-a51f-a005e4bc61d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("48406ef2-d95b-4844-a168-22382567e63c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4fe1def3-b77b-470f-9f63-811154d362b5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("525d6d3d-3336-4131-b297-c5f1629a6ecd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("59b96c52-02e9-48da-91d6-9682922f7fed"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5b1d8b19-d575-43d8-813d-89fe93750bfc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5f28f055-6913-46b2-abaf-5d9cca50e1ce"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6f69cd43-ad9e-4009-acbe-b3bc38eed4a4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6f98168c-4c81-4d33-b0a5-a6e55dc61184"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("722a2415-e3be-43bd-bd17-f7bc2a118fb1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7323ae81-cdd2-41f3-a219-96badbb3ca7e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("757d55d4-ee19-49f3-ac13-429350df1bb6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("75b398b0-81c3-44e0-94df-e9757564f642"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("764def37-c227-4928-9ec4-bacbf01a5909"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("78ab49eb-d509-4708-9343-626185e5f895"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7ebcf384-be57-43b5-8a72-6d92c48c7d44"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7ff5ff68-87ae-4e63-8d9b-e6a7f0af9204"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("81922d82-24f8-4b7a-84ee-7f878968738c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("85bb1391-f3e9-4f0c-93b5-1ad43b37f154"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("86f27aa3-eefc-4006-aae3-7534f07754f2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("87ec98a6-34c8-4f2b-9b59-3a3bbf1af80d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8848b057-0334-4084-aef3-7c97a1b805ab"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8911b285-febd-4c57-9525-a53e222c0c9d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8a0b88c8-0482-4c45-86f5-bf6d8b6a6b98"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8b843c24-812b-45e1-aa77-b48016404945"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8d1d27bc-c9b1-4ad1-9436-1ece7dd79ef2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8ec5ba37-ecbd-4e14-9363-e61a4de9ff95"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("913d7fc0-1d74-4177-a4f2-c0c82bcc7355"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("924eb182-50b7-43c1-82ae-bbd6539986f0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("92ff87ba-b256-4644-b052-fd3bb576d1b8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("96dc8279-7d7b-416d-b56d-51015775c7ab"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9eb43d6e-cd57-45e3-a515-654b13249ac4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9f1df930-2a52-4607-b60c-d295ffd10071"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a2404fcc-2f3c-4941-90ca-ca403f1be0d2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a2fc334d-fe58-4703-a810-f1bf966ab061"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a9ff8555-0a99-43fa-9aa0-a9d8f4683fcd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("aa0245fe-d16c-4503-bc9a-233a5207e38f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("aa266b72-d001-4c54-9517-072b39592428"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ac861c10-caf9-4c59-849d-e6de7e9adda6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("af7b5571-90b1-4771-b538-68eaa8440475"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b0e04240-e971-4a90-a11c-013bb6f30637"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b108df5d-3157-4873-b83f-f6ffb2cf6d41"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b2eef28b-405d-4970-8687-53c8bb1bf450"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b5269a11-01ee-4304-b5bf-714ec56d27d8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b84c1c85-47cb-412b-8555-9d75cd489585"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("baa85653-eab0-45b4-852b-f2e55fa633b5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("bb407d20-42b1-41c8-a3e4-b98af92f6994"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("bbee573b-e5f2-47ec-abc9-5aaf592c3e5a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c0a06261-e8fe-4e2d-8359-6af3825293ad"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c2f3c543-451e-4934-a245-4174351b0ba1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c54ad6a0-5609-4ad0-9f4e-ba4d0a0bd9ca"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c6e70d69-91ed-4ff3-8d5c-afed7522a1a6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c8f1f302-e2d4-4576-b324-a9f55e21605e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c94c0f34-93a5-429d-80c2-818964493997"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c978e9ab-24ab-4485-b5b0-5627d0ae2c66"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cc70e9f5-0fe2-4208-b0e1-16f080562ec1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cda48341-3744-4364-b6f2-01ce8161b081"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d3156ec6-2a9e-4ec2-9b73-2e74a5b5187a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d6331b8a-3ff1-4791-97c1-1c53297e15db"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d7ff8adc-6da6-4863-9b54-d9152b86b520"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d83adeb4-e144-41cf-b680-28b6bd1b2971"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("dace5a19-37df-4224-a3b9-2be16f03c514"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("dc9d99bb-6a28-4ff3-b74a-39a273634d91"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("dea08f11-bd48-4ea2-9aeb-455190a800c5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e342782f-6a62-4f14-a662-d8f503e1714f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e39e354f-d6dd-4148-8261-bc98d21acef8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e579b0f5-2583-463e-b16f-9eaf4c97f37d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e757e4d8-81b5-4ab2-9632-42810c749558"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ef2a4a97-62ec-4c3f-8a0e-ba636084830a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f2e884d3-fce5-4168-b0c1-ee7ea50119d1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f62c7c28-0856-48ba-a211-b3e772243bc5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f7370ed0-db1b-4ab0-8528-8d524868ce78"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fbc5f504-e106-40da-bdc6-cf8aa6f626cd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fdc26186-c983-4f06-a1a3-16b2a7559000"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fecf20b0-9487-4362-827a-6f523227a122"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ffa56e86-8e9f-46de-926a-00a71a50ec7c"));

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserReportRecipe",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserReportComment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "UserBookmarkRecipe",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "RecipeVote",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "CommentVote",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Comment",
                newName: "UserId");

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"),
                column: "Ingredients",
                value: new List<string> { "1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
                column: "Ingredients",
                value: new List<string> { "1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"),
                column: "Ingredients",
                value: new List<string> { "4 Carrots", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
                column: "Ingredients",
                value: new List<string> { "1 Broccoli Head", "200g Cheese", "1 Onion", "2 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
                column: "Ingredients",
                value: new List<string> { "2 cups Milk", "100g Ice Cream", "1 tbsp Sugar" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
                column: "Ingredients",
                value: new List<string> { "4 Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Basil", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
                column: "Ingredients",
                value: new List<string> { "200g Shrimp", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper", "Parsley" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
                column: "Ingredients",
                value: new List<string> { "1 cup Rice", "2 Eggs", "1 Carrot", "Soy Sauce", "2 tbsp Olive Oil", "Garlic" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "50g Cheese", "Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
                column: "Ingredients",
                value: new List<string> { "1 cup Rice", "1 Onion", "2 Garlic Cloves", "1 Carrot", "1 tbsp Soy Sauce", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
                column: "Ingredients",
                value: new List<string> { "200g Spaghetti", "200g Minced Beef", "1 Onion", "2 Garlic Cloves", "4 Tomatoes", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("c8362fc3-5cff-4171-a78d-40613c748596"),
                column: "Ingredients",
                value: new List<string> { "4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"),
                column: "Ingredients",
                value: new List<string> { "200g Mushrooms", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("d2189f90-6991-4901-8195-f0c12d24d900"),
                column: "Ingredients",
                value: new List<string> { "4 Chicken Breasts", "BBQ Sauce", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
                column: "Ingredients",
                value: new List<string> { "2 Eggs", "100g Bacon", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
                column: "Ingredients",
                value: new List<string> { "200g Mushrooms", "100g Cheese", "2 Garlic Cloves", "1 tbsp Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
                column: "Ingredients",
                value: new List<string> { "4 Potatoes", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper" });

            migrationBuilder.UpdateData(
                table: "Recipe",
                keyColumn: "Id",
                keyValue: new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
                column: "Ingredients",
                value: new List<string> { "1 Carrot", "1 Broccoli Head", "Soy Sauce", "Olive Oil", "Salt", "Pepper" });

            migrationBuilder.InsertData(
                table: "Step",
                columns: new[] { "Id", "AttachedImageUrls", "Content", "CreatedAt", "OdinalNumber", "RecipeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("002373cd-c321-4326-bcee-f1376c88e238"), null, "Drain the cooked spaghetti and toss with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("036e4c18-e064-44ea-a4b1-ccbf7b236a07"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04508e64-caab-46bc-9f98-1fc9d0cbabd1"), null, "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0679e33c-43c8-4446-b86a-82a7a38724f2"), null, "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0cbc0088-8ef1-48b9-83d7-55b6c98f9a87"), null, "Melt butter in a pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0d4fc64c-21c8-4521-a6a4-9f3d47db1dc4"), null, "Pour the egg mixture into the pan and cook until set, gently folding the edges.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0f2996e8-b4e9-4761-b9f9-f58b58fb4b83"), null, "Cook for 20 minutes, or until rice is tender and water is absorbed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0f928b79-1146-4e3c-b1cf-542577f02ec7"), null, "Chop the onion, garlic, and tomatoes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("124e8a5b-d758-404a-8e3e-9aebcf0d389b"), null, "Season with salt, pepper, and fresh basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("13a22e3e-fda1-4cd4-8a0f-742d359006a2"), null, "Pour in the vegetable stock and let the soup simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1437c5b2-6bbd-4cf8-88b9-71bb01f6f5f9"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("144884dd-35d7-4112-ace6-0e7141f01e90"), null, "Heat the olive oil in a pan over medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("160404ba-599f-40f8-afe4-dd28c583809f"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1fda2a0e-4478-4d11-8802-864e922b9b59"), null, "Blend the soup until smooth and serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("244f1c9e-5fb2-4173-a1ee-65a7bddf6070"), null, "Peel and devein the shrimp.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("24c65916-5483-4ce2-8aef-aa8bdc66ddfb"), null, "Cook the spaghetti according to package instructions and drain.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("26618d5f-ea76-477c-a342-9086ed61afe0"), null, "Scramble the eggs in a pan with some oil or butter.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("280329aa-2a02-4d4a-9681-6df8c6f6632e"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2dff8b9c-a697-4c87-8a1c-1451d8296e8d"), null, "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2ee6794b-dae6-4f99-a09f-3f8593ee7cfd"), null, "Serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("330adf86-852a-436f-bce8-676595dd2b42"), null, "Add the broccoli and continue stir-frying for another 5 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("34cf5a22-c76d-4521-a3d9-55fa041bca3d"), null, "Add the chopped mushrooms and cook until they release their juices.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("34e86fae-f742-4e0d-a500-b5e91b15a19e"), null, "Boil the potatoes in salted water until tender, about 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("35d9f992-361a-43ef-b717-00ccd7f5e47b"), null, "Add the shrimp to the pan and cook until pink and opaque.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3d797d53-c2fa-49ab-9a4a-595ac606e686"), null, "In a bowl, whisk together the eggs and Parmesan cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3e704a49-bed5-4d87-a5e7-a820858fb699"), null, "Sauté the onion in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("40fa8bca-5273-48fe-84c1-151f08b9e20c"), null, "Season the eggs with salt and pepper, then serve with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("44711740-1ca2-4e4d-ab62-07fa41f0d7c7"), null, "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4ab77ae6-8575-43a3-b960-a63beba88cdc"), null, "Add the rice and soy sauce, then cook for 1-2 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4ae36fed-03a4-453f-ba63-bd55d1388b58"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4be7f0bc-1c02-4a6c-aaec-c545c311d9d5"), null, "Season with salt and pepper to taste, and serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4e534748-482b-48ae-a1f3-223f4087ccad"), null, "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4e67eed5-6b75-4b4d-b78f-b03276459eef"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5099ea31-7c0c-430e-a7e3-3a877368dac1"), null, "Peel and chop the carrots, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50fc3c23-1469-498b-b50d-e70892d1d290"), null, "Blend the soup until smooth, then serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("577f43f5-0096-4490-a8a9-ecb5c65b2ed9"), null, "Season the chicken breasts with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5baa9ad7-5a40-421d-9426-1cb6cc0e536d"), null, "Crack the eggs into the pan and cook to your desired doneness.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("61040e4a-f40d-430c-b201-5bca6f4b64f4"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("61711833-09a9-4bfd-bb97-5759ff04b50f"), null, "Blend until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("634b927e-1539-419a-b33b-c9397bfa0aee"), null, "Remove the stems from the mushrooms and chop them finely.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6410811f-6e12-4cab-b278-4529428d1fb6"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("65308f7a-bfa9-4161-8629-829b3160e772"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("65f0d43f-ac24-4e51-8486-4207fa2f1f28"), null, "Cook the minced beef in a pan until browned.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6c31d210-fce3-4ab0-897b-dfafc5c30271"), null, "Grill the chicken for 6-7 minutes on each side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6dde54cc-4252-462b-b5f0-375a4743cae8"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("71c7eaef-b7e1-45c2-b41e-36dc14af08ef"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("754ce26b-dc93-42f3-8636-34ad860fe730"), null, "Serve with additional BBQ sauce on the side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("75847d5f-b119-48bb-afed-157cc16d377b"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("797e0938-66be-45bd-baca-b02b1375959a"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b244fb8-8c43-4a4f-9764-5e2bbb181177"), null, "Season with salt and pepper to taste.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b726fcc-63c4-4c33-ad94-a3d768be89d3"), null, "Add the cooked rice to the pan and stir-fry with soy sauce.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c768cf5-b351-4341-a11b-f4f47f0f3f1c"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c86e6e3-c9f8-4fd1-b00a-ca4e85f02ac9"), null, "Cook rice according to package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7d4c3896-3f75-4082-bab6-ef9a02fbb673"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80c622f6-c66a-490a-9eb5-32dadffbccd3"), null, "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("814b37a2-ea2d-4f8f-92aa-2f26f939f356"), null, "Serve the Bolognese sauce over the cooked spaghetti.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("820e1fc7-c5ce-48ff-9fbb-8eb77caa95d5"), null, "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("823b58eb-57df-4433-9d8e-45a910636e45"), null, "Season with soy sauce, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8792cf3f-973e-4fde-ae06-204461d2225d"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a31fa0f-a3eb-4f68-a7f0-f67195ff9cc8"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9416ff68-2394-44a3-a7a4-708f0b146eee"), null, "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("96786985-5ea6-4047-acc2-9b26e492ae9e"), null, "Peel and chop the potatoes into chunks.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("98fde936-8738-46ae-b145-bd67692126c3"), null, "Sauté the onions and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fa4a5d6-0bc0-47fe-b408-1c05977dffe7"), null, "Mix the sautéed mushrooms with cheese and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fd7f611-7c49-4ad8-a666-9a37293ca824"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9fd7f7b7-4998-4617-96f3-f48c69e4476c"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a0a168ad-ccd8-4ff6-8cb6-ab24b75d2c87"), null, "Melt butter in a pan and sauté garlic until fragrant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2523826-e1d7-4bde-abe2-ac33dfea5475"), null, "Add milk, ice cream, and sugar to a blender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3ed4267-5379-4dd0-be2b-8a44e8fa3946"), null, "Add the carrots and stir-fry for 3-4 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a88a99b4-cb4c-4890-96df-3825776b3be7"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aa7cf4cb-274f-465e-ae49-246f9cfb78fe"), null, "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ab49ec4f-0b21-403f-b833-835331f23ac1"), null, "Add chopped carrots and garlic to the pan, and cook until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ad637a25-7ccf-424a-bc25-232962c48fbc"), null, "Preheat the grill to medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aee49ef5-0b2b-4112-bfb2-052a9319a78b"), null, "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b0a330f5-d837-45a2-9580-fab8e929292e"), null, "Season with salt and pepper, then blend the soup until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b0d08ac6-0a05-483f-96f1-3449d0c91aa6"), null, "Sauté the onion and garlic in a separate pan until softened.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b3522cd1-49ef-4536-b1a7-a562010a7235"), null, "Cook the spaghetti according to the package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b560505c-a826-4bf9-ac80-00c706a3ff1c"), null, "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b6ccbb8d-3443-4f11-b94d-75f986a91521"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b8f4c726-c5b5-4eed-afe9-dc5bf8941587"), null, "Mash the potatoes with butter, roasted garlic, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("be18a840-49ef-44a1-89a3-8afde331a982"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c0dcf25c-23f9-4561-bb2d-c7112ac4ead4"), null, "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c396fdab-3053-4510-933e-7c7e7d687f32"), null, "Chop the mushrooms, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cacf7182-f273-4ef3-88ca-36872ddde97c"), null, "Season with salt, pepper, and parsley before serving.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("caef8804-734d-4271-a4d1-9b4d54bd282d"), null, "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb1891dd-e82c-4044-99bc-3125b9a07285"), null, "Fry the bacon in a pan until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cb8d0f57-f677-4cdd-b834-63f6989ecf3b"), null, "Fry the bacon in a pan until crispy, then remove and chop.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cdbd4769-a7d4-4309-9a41-11a4b36df8a5"), null, "Chop the onion, garlic, and carrot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d11c1ee9-7860-40ce-b29a-6a564da8b58f"), null, "Chop the carrot and broccoli into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d4e50317-fad9-491f-beb4-0976985eaad6"), null, "Roast the garlic cloves in the oven for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d5d5178c-435c-4e73-a0a4-617756c0a433"), null, "Steam the broccoli until tender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d9bc34d7-8570-48c1-a54d-39dd0d9ddd39"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e23221b0-9f0f-45b2-958c-0ed5b902695e"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2baf9c3-3d8d-4d0d-900f-8a575a2e1968"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2c1c808-a1c9-437d-856e-2e9dbc2a4e1c"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ed3787c0-80a7-414d-bc20-9377037283b4"), null, "Sauté the chopped mushroom stems and garlic in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f56e0f81-b057-4fa2-8eca-6ec89e9d64cb"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3319), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3320) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3264), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3264) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3279), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3296), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3296) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3293), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3302), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3303) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3276), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3277) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3299), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3300) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3286), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3287) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3267), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3268) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3283), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3284) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3270), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3271) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3289), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3308), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3309) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3305), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3306) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3252), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3261) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3273), new DateTime(2024, 12, 27, 17, 35, 52, 458, DateTimeKind.Utc).AddTicks(3274) });
        }
    }
}
