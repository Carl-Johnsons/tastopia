﻿using Contract.Constants;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Domain.Entities;

[Collection("Notification")]
public class Notification : BaseMongoDBAuditableEntity
{
    public Guid TemplateId { get; set; }
    public List<Actor> PrimaryActors { get; set; } = [];
    public List<Actor> SecondaryActors { get; set; } = [];
    [MaxLength(500)]
    public string? ImageUrl { get; set; }
    [MaxLength(500)]
    public string? JsonData { get; set; }
    public List<Recipient> Recipients { get; set; } = [];
    public virtual NotificationTemplate? Template { get; set; }
}

public class Actor
{
    [BsonElement("Id")]
    public string ActorId { get; set; } = null!;
    public EntityType Type { get; set; }
}

public class Recipient
{
    [BsonElement("Id")]
    public Guid RecipientId { get; set; }
    public bool IsViewed { get; set; } = false;
}
