﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.DTOs;
public class CommentRecipeDTO
{
    [Required]
    [JsonProperty("recipeId")]
    public Guid RecipeId { get; set; }
    [Required]
    [JsonProperty("content")]
    [MaxLength(500)]
    public string Content { get; set; } = null!;
}