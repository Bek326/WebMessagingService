﻿namespace WebMessagingService.API.Models;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public int SequenceNumber { get; set; }
}