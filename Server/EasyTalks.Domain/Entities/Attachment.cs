﻿namespace EasyTalks.Domain.Entities
{
    /// <summary>
    /// Вложение (документ)
    /// </summary>
    public class Attachment
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// Подпись
        /// </summary>
        public string Label { get; set; } = String.Empty;

        /// <summary>
        /// Место хранения
        /// </summary>
        public string Path { get; set; } = String.Empty;

        /// <summary>
        /// Сообщение, в котором содержится вложение
        /// </summary>
        public Guid MessageId { get; set; }
        public Message Message { get; set; } = null!;
    }
}