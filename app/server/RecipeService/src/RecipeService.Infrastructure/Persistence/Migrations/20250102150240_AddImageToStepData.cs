using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToStepData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("03cc2a01-fdc4-4a27-b3ee-9cdec7373eda"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("040ab8e6-34ca-4dff-8b2e-9a0084bf80fa"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("043363bd-bf99-4b42-85bd-d83bd4cceadb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("04858259-3b4b-4dc0-bfba-0248464083bb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0737eddc-641d-4808-b609-1f561f750287"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0783f01b-30af-413c-9e13-56d92914e2ff"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("09b2ec3e-512a-41e1-b858-6b91b0a73d04"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0a61e3a1-8482-4d5e-a290-f8258418081a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0b6958c0-c626-4b0b-824f-1f7db3d4314f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0c155164-c75b-40bf-8547-0d9c6827bea8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("11c07ea9-9a85-4671-a83c-af214c720a5a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("11ff2085-eeb2-454a-8c86-180237a9f62e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("19cd4571-c619-4676-83a2-c5be8f72b77c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1a8b527b-305c-4a53-af6c-8ea0c435cffa"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1af73087-d4f2-4547-8af2-9770bd0108db"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1c99a68a-f7ab-4f6a-a06a-187a7ae9b75f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1dfa1819-24a4-49e5-9d7e-f3557f3bb526"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1f09e933-c044-4443-b865-855c138473bf"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("21520076-e5e8-43b6-b8f7-706fdb80e676"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2413e613-c9cb-47c1-8673-03320035bec2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("25209f08-cb5d-46d3-bedb-037c67caa32b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("268f6612-4ebf-4912-84f8-bac35b3eb08b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2aafe2f2-1440-44f2-86a5-2098b199fdaa"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2f8ad875-8b00-4205-9dbe-0e820dbfc065"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2ffe2f8b-ae56-46a6-8d29-daa45737f6e3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("391414d0-f4ae-418f-93cd-9ebe6d675c3f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3a6adb14-cd35-49e2-926f-425db60e9c17"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("409d7156-de4c-41a7-992f-9e891d366139"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("42701515-43b0-44aa-b955-d8b82c0074f2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("47e4410c-f3b1-4bb0-a071-88167cf2c5cf"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("490cb50a-ea08-491f-8b86-63a6cee1cf08"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5888f92c-1c95-4db0-ba46-5b3cf60fe720"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5e49e794-be76-44ef-b701-065fd2536ca4"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("61dbd790-c881-45e9-a570-344a1ff70765"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("63be1739-41c2-4a8f-aa78-8cf5601c05a3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("695e1903-bbed-4dbb-8980-212779faae86"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6a016ab2-144d-4e74-adb7-f03939dd8d98"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6a04f1ce-9fc1-4629-9ee0-fe88e4ceff85"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6c2472a1-feb8-4ccb-9cc0-33e0ac19fe52"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6cdfde37-7b26-4cfd-9d86-925ef57f98bb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("712a9bd4-7e4f-4b3e-8ecc-2f807d0fd443"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7608e24b-037b-497e-847f-1861562fe23c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("785af5c1-9c79-4b6d-a047-30a06da11844"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7a1b7dc9-723e-4e11-b14c-cd8d362a289c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7b08e741-ce7f-4e85-9de3-b968a5ed9f5a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7b44ef67-040f-4bfa-bcce-2dce3b80ebe3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7b5031bc-5d5c-4bb6-962c-0be2670d2825"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7bd6d43b-bba2-4349-b406-3514356ee13b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("816e9a93-60dc-42e4-9143-b7d9c59a7ecb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("830c8bff-7dab-4c8e-bf08-0f7fa0ca2e97"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("882247d8-6c67-4720-8aa3-aa0de77b617f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("88325b80-409b-4257-900d-1af8e964574f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8bc8a0fe-4853-4260-a04e-08c805a77b21"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8c6d94f5-d0d8-46cf-9875-94321614815a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8ca3a023-9a26-44bb-a860-c859799d4391"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("921596eb-f652-407d-916d-8c328cdbf722"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("92879786-5083-4d41-9545-da17309de427"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("932b1513-a637-476c-b313-d2314ecf0f82"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("970a66b2-ca1c-4e8f-a38a-de35f056dbc5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9776db18-a09c-4522-b6e1-ac1a71a6fe21"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("995daf79-11a2-48ae-ab34-4a6bc8ac5a60"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("99f7df90-1271-4a9f-b6c6-bd57f8be04d1"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9a7921dd-4b0a-46fa-9abe-046c7236dd02"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a0f57102-b2a0-485b-bea8-a4bef35f6599"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a6633adc-b3dd-40fa-a13e-5d33b27312cf"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a6a2178f-762f-4c5b-8aa1-29539af3a02c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("aa17ef0c-b89a-4079-ade3-00ea9dc6a807"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ae433bd7-4bc5-4993-9e8c-049b51689f5a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b2bc5524-c4d8-4241-8c24-5aa72f177e9d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b4257b44-4016-4c28-baf9-4ddfd1edef78"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b6db1adc-83c8-489f-ac07-e26909c478d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b863ee9d-c2f1-4f53-8ae1-b00db6d15a78"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("bf6444d3-ac0a-4a4a-8efa-b6a5ff05a585"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c25e127b-eb48-43a5-a3a2-9d7b8f1ea2d7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c7875981-c944-4cf3-b323-05a7b834353e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c8189483-7e19-4e94-8fa3-251f026bee23"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c854738e-9b1b-4ef2-b680-bad8aadc1afe"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("caa27b5e-ebc9-4941-b7e3-ca0304bb1f75"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cbb1bd78-8a35-4f87-8228-5e6f817190dd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cecfbde6-e37a-479d-8c63-8944d7394dd9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d06cd4fa-c1d0-43fa-8450-1d33595ec30b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d2b604ef-2dd4-46a7-ac00-9d53cf249a84"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("dc79ec03-c8df-4ba2-ae75-c368050d2cc8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("dd086d3b-911e-47f2-a4a1-a52caa62053b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e60c0c6a-d281-44be-8079-dfb48a149a41"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ed3934b3-3ca5-4aee-ab47-fcbea9810740"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ed5db2c9-90b7-4c7c-96f7-158be3907998"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f09b9840-642e-4cb1-a5ed-8bbeb6eb55af"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f246a6b5-4639-4faa-8a2b-4d449ee2b747"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f48e0348-d0ee-473d-8954-be9cddbbf7d9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f6601717-905f-4b0a-ad05-c876b179d367"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f6cd5add-3502-4d69-a15e-6c0d2228656e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f6e6bfcd-bbca-45ce-9ed6-f76ce85246f0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f92bcb45-a3d4-4316-abc4-94e5554db040"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fa210d3f-43b5-4751-b088-57e87ca22660"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("faea22b5-8fb0-4ae8-a60a-200ec1d9385e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fdae81ac-1555-4754-a6fd-c35a8b5c873c"));

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
                    { new Guid("00b41264-a30a-4fef-ab65-a76d8779e979"), null, "Serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("02314958-d371-4dc3-9d71-89a6762af6d5"), new List<string> { "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-02.jpg" }, "Add milk, ice cream, and sugar to a blender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0bfc9838-9b70-4cca-b6e8-fa1a2733621f"), new List<string> { "https://www.eatingwell.com/thmb/uIS7xz8ZcT6WLalfYHxEvpLJF9Y=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Hot-Grill-98dc8b55c76b427b9a026cf509ec7c48.jpg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXHzHjU_DYlhDHtkb3zr_f28KMRHIH-7Lgkg&s", "https://blog.zgrills.com/wp-content/uploads/2022/09/what-temperature-is-medium-heat-on-a-grill.jpg" }, "Preheat the grill to medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0cb0388b-ecd1-40ca-a4ac-b89e4cee9f92"), null, "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11b3dd51-ff65-42cd-9769-b5b7e729ea6e"), null, "Cook rice according to package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("15a38f91-6cba-4438-98f4-fd5470defee9"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("17caadb9-2342-4186-8ea8-e2c8dec2f745"), null, "Season with salt and pepper to taste, and serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("185bf5c8-bbc8-4938-9401-77f21ce2dd4b"), null, "Add the rice and soy sauce, then cook for 1-2 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("19735a2e-69c9-4620-a8b4-a80d39b8c9b2"), null, "Blend the soup until smooth, then serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("19bc6404-c623-47df-afd2-e704bdbf3fa0"), null, "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("19e074a4-db70-4eff-a3ed-bdc540f6bfa6"), null, "Sauté the onion and garlic in a separate pan until softened.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1c86a177-5095-48d5-bd2a-b48487ae102c"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1c86eb97-caad-423f-a3ec-06289fe0c620"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1e8a066e-c445-41ca-a120-058252cf208f"), null, "Season with soy sauce, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("21043361-05cb-4607-a3cf-94633adf4e30"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("21442754-d1e9-4363-afd6-d75b8f9422f2"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("21fade0b-8631-4fd9-8df1-118b0dfbc318"), null, "Mix the sautéed mushrooms with cheese and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("237d3a4d-4218-41d3-96f4-0edb940b2545"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("23dace0c-5a9b-40db-bbe6-f100f7e8c402"), null, "Blend the soup until smooth and serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("24a44a23-141c-44c3-b492-f6f07f50a8ff"), null, "Scramble the eggs in a pan with some oil or butter.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("25d85db6-25fb-4ceb-883d-03558dd5f3fc"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2aeedb04-a793-4a15-a4f4-9ec2d7b9b4c6"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("330533bf-06df-46ad-a389-a6f2ef16adf6"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3597097d-3e76-4796-9d30-7aafb6c4cf56"), null, "Pour in the vegetable stock and let the soup simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("36f0b132-52a9-4c8b-ab3f-efe6e1bd92cd"), null, "Chop the mushrooms, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("38a7af35-f0d9-4cde-852d-d9d469f453a5"), null, "Boil the potatoes in salted water until tender, about 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("38c182d3-2be5-4559-85c9-a929992e907c"), new List<string> { "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-04.jpg" }, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("39256150-2f49-4b3d-86e4-d39eb8033643"), null, "Mash the potatoes with butter, roasted garlic, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("395319f7-2369-4499-b0e7-4308dd0f35ca"), new List<string> { "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-03.jpg" }, "Blend until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3a73a259-04e6-42ae-90b0-96bc2154544d"), null, "Chop the onion, garlic, and tomatoes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("42a81e49-cbf7-4dd3-ba3c-3a0f34f5fee7"), null, "Add the chopped mushrooms and cook until they release their juices.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("457e9c18-cd78-4c51-bc45-d84fcbdb003f"), null, "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("486f4773-6d7d-41d0-b9c0-b1fa8e442aeb"), null, "Cook the minced beef in a pan until browned.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4d49c3d7-834a-4131-8825-e517f5495986"), null, "Cook for 20 minutes, or until rice is tender and water is absorbed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50579961-817a-4816-9b3c-cecf66893b18"), null, "Crack the eggs into the pan and cook to your desired doneness.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("50ea4337-2f06-438e-89c6-71236854774f"), null, "In a bowl, whisk together the eggs and Parmesan cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("511525de-9c9e-484d-a91e-ae61ba6aebd6"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("524253d2-fa84-49d3-bf2f-a07390527df8"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5302dd36-5ae6-4b5c-a8dc-62882c91811b"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5447fb1f-4550-494d-9319-469c36b2de68"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("553102d1-130d-4bd1-810f-36124033aa46"), null, "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("56e3b694-25fa-4b9b-b488-7df9f6828050"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("58796271-10cb-4dac-8416-62603efc6bf6"), null, "Sauté the onions and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5baf2843-dc5a-4876-935d-d59cd1e78518"), null, "Season with salt, pepper, and fresh basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5f3c165d-e022-438d-807b-c48aabab133a"), null, "Add the carrots and stir-fry for 3-4 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6430c65c-ee56-4b72-9d31-2ed6b2abda60"), null, "Peel and chop the potatoes into chunks.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("67e6432c-4fde-48c8-b963-4aeb8ae37e4e"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("67e978fb-81d0-472a-815e-404d94398dda"), null, "Season with salt, pepper, and parsley before serving.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("689af6ca-6576-4431-bf7b-03f6c47a1ac9"), null, "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6aba3ad1-3328-4f22-90ef-9c47c2604e23"), null, "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6de3757e-b554-4c06-8c3f-55540c923292"), null, "Heat the olive oil in a pan over medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6ec76994-753b-43e6-bc52-c5dcfc1afddc"), null, "Chop the carrot and broccoli into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("728095e8-2bcf-4173-a124-d32239f89547"), null, "Season with salt and pepper, then blend the soup until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c57ddaf-0c24-4367-a6e0-937d62b1543a"), null, "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7e2fed63-1ad3-4728-b283-b68ba2a904f8"), new List<string> { "https://i2.wp.com/www.downshiftology.com/wp-content/uploads/2022/06/BBQ-Chicken-11.jpg" }, "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7f4226a7-e3f4-43ac-b954-fe7749cf6da5"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8e916d40-e27c-4c02-9f7b-6050eab315b0"), null, "Fry the bacon in a pan until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("91d94ca1-20e6-46e7-b99b-2976360eacbc"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("942e62db-4202-471f-9554-a467fcb04e45"), null, "Roast the garlic cloves in the oven for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("975761dd-db7e-4e1a-a467-db0a6460fb36"), null, "Sauté the onion in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9795f0e4-ed23-471c-a694-95e9dfe979ad"), null, "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("99c7e715-5637-4d18-aaf5-38a185d48738"), null, "Sauté the chopped mushroom stems and garlic in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9a24971f-984e-4574-8f9f-4a3ab3dd5679"), null, "Melt butter in a pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9a604a99-4b5e-4840-8709-64a30352019b"), null, "Peel and chop the carrots, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9f38d837-957c-4c53-8a43-2fd01f7a600f"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3415616-27bb-4d1e-8a97-0c45e8bc3723"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a5cc6881-a348-4ecb-9d54-06bfe8ac7d0f"), null, "Pour the egg mixture into the pan and cook until set, gently folding the edges.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a6799d1d-2bac-4d4d-a390-51e4b38a7a87"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ac0b24ef-b3ec-4d35-bc96-36508ba42160"), null, "Drain the cooked spaghetti and toss with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ad612e04-597f-4d82-8827-383fa0f01080"), null, "Melt butter in a pan and sauté garlic until fragrant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b1b7f53c-dfb6-4bff-8dfa-45c2a980f929"), new List<string> { "https://thewholeserving.com/wp-content/uploads/2024/04/Platter-of-prepared-drumsticks-with-a-glass-container-of-extra-barbecue-sauce-on-the-right-side-of-the-platter.jpg" }, "Serve with additional BBQ sauce on the side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b1df34c0-fa5b-43cd-8d2d-002c8234c235"), null, "Season the eggs with salt and pepper, then serve with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b21466ff-2251-449f-b130-ded465f21975"), null, "Cook the spaghetti according to package instructions and drain.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b3f3efa0-7134-44f0-9b6e-e7ae57e154f0"), null, "Add chopped carrots and garlic to the pan, and cook until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ba16b2f8-ccc7-4ebe-8df1-4fffac83acf6"), null, "Fry the bacon in a pan until crispy, then remove and chop.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bb332714-ab22-4eee-83de-76f950b734a0"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("be6282b9-d0b3-4c8e-9a10-1fd144e0eb37"), null, "Add the shrimp to the pan and cook until pink and opaque.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c1ffab3f-c713-474c-9c0a-b17a2e3759af"), null, "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c5756b3f-dbb0-4c39-867b-3a00f25e4911"), null, "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c589c4af-b746-414a-9a48-a0ca8e2fe596"), null, "Peel and devein the shrimp.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ce1f5aa8-b6ff-4089-b955-f1fc04d18c91"), null, "Add the broccoli and continue stir-frying for another 5 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cf9ee711-4e90-409b-8cc5-4f8267b67d51"), null, "Remove the stems from the mushrooms and chop them finely.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cfdcfb1c-fc0e-40a0-a056-85a021caf93e"), null, "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d1a84712-0e21-4f53-bf45-3e762bd44516"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d3e76be4-1213-401d-a1bb-37884a480d3b"), null, "Season with salt and pepper to taste.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d8f7c429-a60e-47fb-97a1-d7ddca709015"), null, "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e17f9a34-7e0a-46f2-934b-18a0e15f64d7"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e29b9819-1004-4058-9b1f-6abe2e1bd9d3"), null, "Steam the broccoli until tender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e719018b-d5fd-4583-8772-921d3ea9ae0a"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e81bbb4a-6b9f-4788-98d4-d76147b42718"), null, "Add the cooked rice to the pan and stir-fry with soy sauce.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e898d6aa-9b82-4f06-82fc-81f823b9f397"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ece4402d-1186-43c6-84a6-cf6808c0741d"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f19b56fb-77bb-4117-8be2-9128d81dd824"), null, "Chop the onion, garlic, and carrot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f3814d68-fc1f-4a41-870a-0a782822d1b7"), new List<string> { "https://cdn.apartmenttherapy.info/image/upload/f_auto,q_auto:eco,c_fill,g_auto,w_1690,h_1128/k%2Farchive%2Fa0d25ce80ff3b94487b4df0f5bd83fb943b7d0b2", "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/baked-skin-on-chicken-breast-ingredients.jpg", "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/salt-and-pepper.jpg" }, "Season the chicken breasts with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f879763d-e082-4f4c-9ef6-b40e1786769e"), new List<string> { "https://s3.festivalfoods.net/blog/uploads/2022/05/IMG_0864.jpeg", "https://cdn.shopify.com/s/files/1/0271/5287/5653/files/20240512131902-holychicken.jpg?v=1715519944&width=1600&height=900" }, "Grill the chicken for 6-7 minutes on each side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fd63b80e-5368-44f5-8f21-d41e962aa11d"), null, "Cook the spaghetti according to the package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fe89311e-618b-49b5-9808-5d5b08f2eee2"), null, "Serve the Bolognese sauce over the cooked spaghetti.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5353), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5353) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5305), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5305) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5323), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5324) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5339), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5337), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5337) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5344), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5345) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5342), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5342) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5308), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5309) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5327), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5327) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5313), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5314) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5334), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5334) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5349), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5347), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5347) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5289), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5301) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5316), new DateTime(2025, 1, 2, 15, 2, 39, 477, DateTimeKind.Utc).AddTicks(5317) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("00b41264-a30a-4fef-ab65-a76d8779e979"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("02314958-d371-4dc3-9d71-89a6762af6d5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0bfc9838-9b70-4cca-b6e8-fa1a2733621f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("0cb0388b-ecd1-40ca-a4ac-b89e4cee9f92"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("11b3dd51-ff65-42cd-9769-b5b7e729ea6e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("15a38f91-6cba-4438-98f4-fd5470defee9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("17caadb9-2342-4186-8ea8-e2c8dec2f745"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("185bf5c8-bbc8-4938-9401-77f21ce2dd4b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("19735a2e-69c9-4620-a8b4-a80d39b8c9b2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("19bc6404-c623-47df-afd2-e704bdbf3fa0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("19e074a4-db70-4eff-a3ed-bdc540f6bfa6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1c86a177-5095-48d5-bd2a-b48487ae102c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1c86eb97-caad-423f-a3ec-06289fe0c620"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("1e8a066e-c445-41ca-a120-058252cf208f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("21043361-05cb-4607-a3cf-94633adf4e30"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("21442754-d1e9-4363-afd6-d75b8f9422f2"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("21fade0b-8631-4fd9-8df1-118b0dfbc318"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("237d3a4d-4218-41d3-96f4-0edb940b2545"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("23dace0c-5a9b-40db-bbe6-f100f7e8c402"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("24a44a23-141c-44c3-b492-f6f07f50a8ff"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("25d85db6-25fb-4ceb-883d-03558dd5f3fc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("2aeedb04-a793-4a15-a4f4-9ec2d7b9b4c6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("330533bf-06df-46ad-a389-a6f2ef16adf6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3597097d-3e76-4796-9d30-7aafb6c4cf56"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("36f0b132-52a9-4c8b-ab3f-efe6e1bd92cd"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("38a7af35-f0d9-4cde-852d-d9d469f453a5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("38c182d3-2be5-4559-85c9-a929992e907c"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("39256150-2f49-4b3d-86e4-d39eb8033643"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("395319f7-2369-4499-b0e7-4308dd0f35ca"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("3a73a259-04e6-42ae-90b0-96bc2154544d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("42a81e49-cbf7-4dd3-ba3c-3a0f34f5fee7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("457e9c18-cd78-4c51-bc45-d84fcbdb003f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("486f4773-6d7d-41d0-b9c0-b1fa8e442aeb"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("4d49c3d7-834a-4131-8825-e517f5495986"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("50579961-817a-4816-9b3c-cecf66893b18"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("50ea4337-2f06-438e-89c6-71236854774f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("511525de-9c9e-484d-a91e-ae61ba6aebd6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("524253d2-fa84-49d3-bf2f-a07390527df8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5302dd36-5ae6-4b5c-a8dc-62882c91811b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5447fb1f-4550-494d-9319-469c36b2de68"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("553102d1-130d-4bd1-810f-36124033aa46"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("56e3b694-25fa-4b9b-b488-7df9f6828050"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("58796271-10cb-4dac-8416-62603efc6bf6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5baf2843-dc5a-4876-935d-d59cd1e78518"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("5f3c165d-e022-438d-807b-c48aabab133a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6430c65c-ee56-4b72-9d31-2ed6b2abda60"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("67e6432c-4fde-48c8-b963-4aeb8ae37e4e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("67e978fb-81d0-472a-815e-404d94398dda"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("689af6ca-6576-4431-bf7b-03f6c47a1ac9"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6aba3ad1-3328-4f22-90ef-9c47c2604e23"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6de3757e-b554-4c06-8c3f-55540c923292"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("6ec76994-753b-43e6-bc52-c5dcfc1afddc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("728095e8-2bcf-4173-a124-d32239f89547"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7c57ddaf-0c24-4367-a6e0-937d62b1543a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7e2fed63-1ad3-4728-b283-b68ba2a904f8"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("7f4226a7-e3f4-43ac-b954-fe7749cf6da5"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("8e916d40-e27c-4c02-9f7b-6050eab315b0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("91d94ca1-20e6-46e7-b99b-2976360eacbc"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("942e62db-4202-471f-9554-a467fcb04e45"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("975761dd-db7e-4e1a-a467-db0a6460fb36"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9795f0e4-ed23-471c-a694-95e9dfe979ad"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("99c7e715-5637-4d18-aaf5-38a185d48738"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9a24971f-984e-4574-8f9f-4a3ab3dd5679"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9a604a99-4b5e-4840-8709-64a30352019b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("9f38d837-957c-4c53-8a43-2fd01f7a600f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a3415616-27bb-4d1e-8a97-0c45e8bc3723"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a5cc6881-a348-4ecb-9d54-06bfe8ac7d0f"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("a6799d1d-2bac-4d4d-a390-51e4b38a7a87"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ac0b24ef-b3ec-4d35-bc96-36508ba42160"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ad612e04-597f-4d82-8827-383fa0f01080"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b1b7f53c-dfb6-4bff-8dfa-45c2a980f929"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b1df34c0-fa5b-43cd-8d2d-002c8234c235"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b21466ff-2251-449f-b130-ded465f21975"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("b3f3efa0-7134-44f0-9b6e-e7ae57e154f0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ba16b2f8-ccc7-4ebe-8df1-4fffac83acf6"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("bb332714-ab22-4eee-83de-76f950b734a0"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("be6282b9-d0b3-4c8e-9a10-1fd144e0eb37"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c1ffab3f-c713-474c-9c0a-b17a2e3759af"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c5756b3f-dbb0-4c39-867b-3a00f25e4911"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("c589c4af-b746-414a-9a48-a0ca8e2fe596"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ce1f5aa8-b6ff-4089-b955-f1fc04d18c91"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cf9ee711-4e90-409b-8cc5-4f8267b67d51"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("cfdcfb1c-fc0e-40a0-a056-85a021caf93e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d1a84712-0e21-4f53-bf45-3e762bd44516"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d3e76be4-1213-401d-a1bb-37884a480d3b"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("d8f7c429-a60e-47fb-97a1-d7ddca709015"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e17f9a34-7e0a-46f2-934b-18a0e15f64d7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e29b9819-1004-4058-9b1f-6abe2e1bd9d3"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e719018b-d5fd-4583-8772-921d3ea9ae0a"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e81bbb4a-6b9f-4788-98d4-d76147b42718"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("e898d6aa-9b82-4f06-82fc-81f823b9f397"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("ece4402d-1186-43c6-84a6-cf6808c0741d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f19b56fb-77bb-4117-8be2-9128d81dd824"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f3814d68-fc1f-4a41-870a-0a782822d1b7"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("f879763d-e082-4f4c-9ef6-b40e1786769e"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fd63b80e-5368-44f5-8f21-d41e962aa11d"));

            migrationBuilder.DeleteData(
                table: "Step",
                keyColumn: "Id",
                keyValue: new Guid("fe89311e-618b-49b5-9808-5d5b08f2eee2"));

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
                    { new Guid("03cc2a01-fdc4-4a27-b3ee-9cdec7373eda"), null, "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("040ab8e6-34ca-4dff-8b2e-9a0084bf80fa"), null, "Toss cooked spaghetti with bacon and remove from heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("043363bd-bf99-4b42-85bd-d83bd4cceadb"), null, "Serve with additional BBQ sauce on the side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("04858259-3b4b-4dc0-bfba-0248464083bb"), null, "Add chopped carrots and garlic to the pan, and cook until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0737eddc-641d-4808-b609-1f561f750287"), null, "Cook the spaghetti according to the package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0783f01b-30af-413c-9e13-56d92914e2ff"), null, "Peel and chop the potatoes into chunks.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("09b2ec3e-512a-41e1-b858-6b91b0a73d04"), null, "Sauté the onion in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0a61e3a1-8482-4d5e-a290-f8258418081a"), null, "Pour in the vegetable stock and let the soup simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0b6958c0-c626-4b0b-824f-1f7db3d4314f"), null, "Fry the bacon in a pan until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0c155164-c75b-40bf-8547-0d9c6827bea8"), null, "Blend the soup until smooth and serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11c07ea9-9a85-4671-a83c-af214c720a5a"), null, "Chop the mushrooms, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("11ff2085-eeb2-454a-8c86-180237a9f62e"), null, "Grill the chicken for 6-7 minutes on each side.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("19cd4571-c619-4676-83a2-c5be8f72b77c"), null, "Add the broccoli and continue stir-frying for another 5 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1a8b527b-305c-4a53-af6c-8ea0c435cffa"), null, "Mix softened butter with minced garlic and parsley.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1af73087-d4f2-4547-8af2-9770bd0108db"), null, "Season the chicken breasts with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1c99a68a-f7ab-4f6a-a06a-187a7ae9b75f"), null, "Mash the potatoes with butter, roasted garlic, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1dfa1819-24a4-49e5-9d7e-f3557f3bb526"), null, "Cook the minced beef in a pan until browned.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1f09e933-c044-4443-b865-855c138473bf"), null, "Chop all vegetables into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("21520076-e5e8-43b6-b8f7-706fdb80e676"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2413e613-c9cb-47c1-8673-03320035bec2"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("25209f08-cb5d-46d3-bedb-037c67caa32b"), null, "Pour the egg mixture into the pan and gently stir until softly set.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("268f6612-4ebf-4912-84f8-bac35b3eb08b"), null, "In a bowl, whisk together the eggs and Parmesan cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2aafe2f2-1440-44f2-86a5-2098b199fdaa"), null, "Scramble the eggs in a pan with some oil or butter.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2f8ad875-8b00-4205-9dbe-0e820dbfc065"), null, "Crack the eggs into the pan and cook to your desired doneness.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2ffe2f8b-ae56-46a6-8d29-daa45737f6e3"), null, "Add the rice and soy sauce, then cook for 1-2 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("391414d0-f4ae-418f-93cd-9ebe6d675c3f"), null, "Fry the bacon in a pan until crispy, then remove and chop.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3a6adb14-cd35-49e2-926f-425db60e9c17"), null, "Melt butter in a non-stick pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("409d7156-de4c-41a7-992f-9e891d366139"), null, "Chop the onion, garlic, and tomatoes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("42701515-43b0-44aa-b955-d8b82c0074f2"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("47e4410c-f3b1-4bb0-a071-88167cf2c5cf"), null, "Add the carrots and stir-fry for 3-4 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("490cb50a-ea08-491f-8b86-63a6cee1cf08"), null, "Melt butter in a pan and sauté garlic until fragrant.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5888f92c-1c95-4db0-ba46-5b3cf60fe720"), null, "Heat olive oil in a wok or large pan.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5e49e794-be76-44ef-b701-065fd2536ca4"), null, "Season with salt, pepper, and parsley before serving.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("61dbd790-c881-45e9-a570-344a1ff70765"), null, "Sauté the onions and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("63be1739-41c2-4a8f-aa78-8cf5601c05a3"), null, "Mix eggs and grated parmesan in a bowl.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("695e1903-bbed-4dbb-8980-212779faae86"), null, "Blend the soup until smooth, then serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6a016ab2-144d-4e74-adb7-f03939dd8d98"), null, "Season with salt and pepper to taste.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6a04f1ce-9fc1-4629-9ee0-fe88e4ceff85"), null, "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6c2472a1-feb8-4ccb-9cc0-33e0ac19fe52"), null, "Cook the spaghetti according to package instructions and drain.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6cdfde37-7b26-4cfd-9d86-925ef57f98bb"), null, "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("712a9bd4-7e4f-4b3e-8ecc-2f807d0fd443"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7608e24b-037b-497e-847f-1861562fe23c"), null, "Cook rice according to package instructions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("785af5c1-9c79-4b6d-a047-30a06da11844"), null, "Cook spaghetti in salted boiling water until al dente.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7a1b7dc9-723e-4e11-b14c-cd8d362a289c"), null, "Roast the garlic cloves in the oven for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b08e741-ce7f-4e85-9de3-b968a5ed9f5a"), null, "Preheat the oven to 180°C (350°F).", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b44ef67-040f-4bfa-bcce-2dce3b80ebe3"), null, "Mix the sautéed mushrooms with cheese and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b5031bc-5d5c-4bb6-962c-0be2670d2825"), null, "Cook for 20 minutes, or until rice is tender and water is absorbed.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7bd6d43b-bba2-4349-b406-3514356ee13b"), null, "Sauté the chopped mushroom stems and garlic in butter until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("816e9a93-60dc-42e4-9143-b7d9c59a7ecb"), null, "Season the eggs with salt and pepper, then serve with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("830c8bff-7dab-4c8e-bf08-0f7fa0ca2e97"), null, "Peel and devein the shrimp.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("882247d8-6c67-4720-8aa3-aa0de77b617f"), null, "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("88325b80-409b-4257-900d-1af8e964574f"), null, "Sauté the onion and garlic in a separate pan until softened.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8bc8a0fe-4853-4260-a04e-08c805a77b21"), null, "Blend the soup until smooth and season with salt and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8c6d94f5-d0d8-46cf-9875-94321614815a"), null, "Remove the stems from the mushrooms and chop them finely.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8ca3a023-9a26-44bb-a860-c859799d4391"), null, "Preheat the grill to medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("921596eb-f652-407d-916d-8c328cdbf722"), null, "Chop the onion, garlic, and carrot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("92879786-5083-4d41-9545-da17309de427"), null, "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("932b1513-a637-476c-b313-d2314ecf0f82"), null, "Add tomatoes and vegetable stock, then simmer for 30 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("970a66b2-ca1c-4e8f-a38a-de35f056dbc5"), null, "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9776db18-a09c-4522-b6e1-ac1a71a6fe21"), null, "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("995daf79-11a2-48ae-ab34-4a6bc8ac5a60"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("99f7df90-1271-4a9f-b6c6-bd57f8be04d1"), null, "Add soy sauce and stir well.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9a7921dd-4b0a-46fa-9abe-046c7236dd02"), null, "Season with salt and pepper to taste, and serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a0f57102-b2a0-485b-bea8-a4bef35f6599"), null, "Boil the potatoes in salted water until tender, about 15 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a6633adc-b3dd-40fa-a13e-5d33b27312cf"), null, "Season with salt, pepper, and fresh basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a6a2178f-762f-4c5b-8aa1-29539af3a02c"), null, "Heat the olive oil in a pan over medium-high heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("aa17ef0c-b89a-4079-ade3-00ea9dc6a807"), null, "Sauté the onion and garlic in a pot with some oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ae433bd7-4bc5-4993-9e8c-049b51689f5a"), null, "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("d2189f90-6991-4901-8195-f0c12d24d900"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b2bc5524-c4d8-4241-8c24-5aa72f177e9d"), null, "Serve immediately.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b4257b44-4016-4c28-baf9-4ddfd1edef78"), null, "Add the egg mixture and stir until creamy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b6db1adc-83c8-489f-ac07-e26909c478d5"), null, "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b863ee9d-c2f1-4f53-8ae1-b00db6d15a78"), null, "Sauté onion and garlic in olive oil until soft.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("c8362fc3-5cff-4171-a78d-40613c748596"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("bf6444d3-ac0a-4a4a-8efa-b6a5ff05a585"), null, "Bake in the oven for 10-12 minutes until golden.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c25e127b-eb48-43a5-a3a2-9d7b8f1ea2d7"), null, "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("aa626791-ee53-4390-a5a5-94c5b8096f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c7875981-c944-4cf3-b323-05a7b834353e"), null, "Steam the broccoli until tender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c8189483-7e19-4e94-8fa3-251f026bee23"), null, "Chop the tomatoes, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c854738e-9b1b-4ef2-b680-bad8aadc1afe"), null, "Drain the cooked spaghetti and toss with the crispy bacon.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("5c2c8d61-4474-4933-9f70-f016e45e5f1d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("caa27b5e-ebc9-4941-b7e3-ca0304bb1f75"), null, "Add the chopped mushrooms and cook until they release their juices.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cbb1bd78-8a35-4f87-8228-5e6f817190dd"), null, "Blend until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("cecfbde6-e37a-479d-8c63-8944d7394dd9"), null, "Fry bacon until crispy.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d06cd4fa-c1d0-43fa-8450-1d33595ec30b"), null, "Add milk, ice cream, and sugar to a blender.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d2b604ef-2dd4-46a7-ac00-9d53cf249a84"), null, "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dc79ec03-c8df-4ba2-ae75-c368050d2cc8"), null, "Chop the carrot and broccoli into bite-sized pieces.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dd086d3b-911e-47f2-a4a1-a52caa62053b"), null, "Pour the egg mixture into the pan and cook until set, gently folding the edges.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e60c0c6a-d281-44be-8079-dfb48a149a41"), null, "Season with salt and pepper, then blend the soup until smooth.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("d1672c31-64cc-44b5-9630-2e7f9f651234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ed3934b3-3ca5-4aee-ab47-fcbea9810740"), null, "Add vegetables and stir-fry for 5-7 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ed5db2c9-90b7-4c7c-96f7-158be3907998"), null, "Peel and chop the carrots, onion, and garlic.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("1f337cae-879e-4d85-9eb9-658b18f5beff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f09b9840-642e-4cb1-a5ed-8bbeb6eb55af"), null, "Serve the Bolognese sauce over the cooked spaghetti.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f246a6b5-4639-4faa-8a2b-4d449ee2b747"), null, "Serve hot.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f48e0348-d0ee-473d-8954-be9cddbbf7d9"), null, "Season with soy sauce, salt, and pepper.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f6601717-905f-4b0a-ad05-c876b179d367"), null, "Add the cooked rice to the pan and stir-fry with soy sauce.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f6cd5add-3502-4d69-a15e-6c0d2228656e"), null, "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new Guid("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f6e6bfcd-bbca-45ce-9ed6-f76ce85246f0"), null, "Spread the mixture onto sliced baguette.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("057aa844-742a-4952-8162-dbfbd7e493ac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f92bcb45-a3d4-4316-abc4-94e5554db040"), null, "Melt butter in a pan over medium heat.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("9e9b3a16-42f1-40a3-9f60-e704e632b609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fa210d3f-43b5-4751-b088-57e87ca22660"), null, "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new Guid("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("faea22b5-8fb0-4ae8-a60a-200ec1d9385e"), null, "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fdae81ac-1555-4754-a6fd-c35a8b5c873c"), null, "Add the shrimp to the pan and cook until pink and opaque.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new Guid("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("042dd338-3338-45a1-96c6-a1e2d0ca71ff"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(391), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(392) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("2bf7f026-e745-4bd9-8701-a9519742d0f7"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(314), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(315) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("3e084d1f-4dd1-42dc-9a15-9f8fbb4b8495"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(336), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("4a6fc1f9-7f8d-49de-85b4-b6b9f8d1c4a8"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(363), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(364) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("63af97d6-9fc0-4c5b-b6b1-d5e5e8b8a0a6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(358), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(359) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6c91b894-d6dd-4c9b-a106-bfd029ce9e16"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(372), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(373) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("6f229db7-e0d7-4fd8-83d6-4f8b9c3ef5c1"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(332), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(333) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("71676633-493e-46c5-86a0-21773f196035"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(367), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(368) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("8db97f13-4ff0-4a0e-9c5c-b2c1d4e1b78e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(345), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("92316e11-fd87-4c0f-aac7-bde4f19c2b38"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(318), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(319) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("9c5d4e5f-3c44-4bde-a5a6-7a1d3e1b67d6"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(341), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(342) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("a7a1953d-027e-43b6-ad0d-d10312a3064d"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(323), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(324) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("ad7dca4b-9ae8-44d3-b4c3-7d4c9e9b6f8e"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(350), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(351) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("d8b74fc2-f848-41af-a53f-20170aa453cd"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(381), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(382) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("de3698bf-ad8b-4cc6-8ed0-662bd7eca486"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(377), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("df3f6301-3cae-480a-87da-c7b8f6150292"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(289), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(309) });

            migrationBuilder.UpdateData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: new Guid("f92b7c2e-d2f0-4b7f-b5d5-9d3a41b718c3"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(327), new DateTime(2024, 12, 28, 16, 18, 57, 939, DateTimeKind.Utc).AddTicks(328) });
        }
    }
}
