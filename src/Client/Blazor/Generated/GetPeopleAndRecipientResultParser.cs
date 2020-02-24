﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace Client
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetPeopleAndRecipientResultParser
        : JsonResultParserBase<IGetPeopleAndRecipient>
    {
        private readonly IValueSerializer _iDSerializer;
        private readonly IValueSerializer _stringSerializer;
        private readonly IValueSerializer _urlSerializer;
        private readonly IValueSerializer _booleanSerializer;
        private readonly IValueSerializer _dateTimeSerializer;
        private readonly IValueSerializer _directionSerializer;

        public GetPeopleAndRecipientResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _iDSerializer = serializerResolver.Get("ID");
            _stringSerializer = serializerResolver.Get("String");
            _urlSerializer = serializerResolver.Get("Url");
            _booleanSerializer = serializerResolver.Get("Boolean");
            _dateTimeSerializer = serializerResolver.Get("DateTime");
            _directionSerializer = serializerResolver.Get("Direction");
        }

        protected override IGetPeopleAndRecipient ParserData(JsonElement data)
        {
            return new GetPeopleAndRecipient
            (
                ParseGetPeopleAndRecipientPeople(data, "people"),
                ParseGetPeopleAndRecipientPersonById(data, "personById")
            );

        }

        private IPersonConnection ParseGetPeopleAndRecipientPeople(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return new PersonConnection
            (
                ParseGetPeopleAndRecipientPeopleNodes(obj, "nodes")
            );
        }

        private IRecipient ParseGetPeopleAndRecipientPersonById(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Recipient
            (
                ParseGetPeopleAndRecipientPersonByIdMessages(obj, "messages"),
                DeserializeID(obj, "id"),
                DeserializeString(obj, "name"),
                DeserializeString(obj, "email"),
                DeserializeNullableUrl(obj, "imageUri"),
                DeserializeBoolean(obj, "isOnline"),
                DeserializeDateTime(obj, "lastSeen")
            );
        }

        private IReadOnlyList<IPerson> ParseGetPeopleAndRecipientPeopleNodes(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new IPerson[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new Person
                (
                    DeserializeID(element, "id"),
                    DeserializeString(element, "name"),
                    DeserializeString(element, "email"),
                    DeserializeNullableUrl(element, "imageUri"),
                    DeserializeBoolean(element, "isOnline"),
                    DeserializeDateTime(element, "lastSeen")
                );

            }

            return list;
        }

        private IMessageConnection ParseGetPeopleAndRecipientPersonByIdMessages(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return new MessageConnection
            (
                ParseGetPeopleAndRecipientPersonByIdMessagesNodes(obj, "nodes")
            );
        }

        private IReadOnlyList<IMessage> ParseGetPeopleAndRecipientPersonByIdMessagesNodes(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new IMessage[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new Message
                (
                    DeserializeDirection(element, "direction"),
                    DeserializeID(element, "id"),
                    ParseGetPeopleAndRecipientPersonByIdMessagesNodesRecipient(element, "recipient"),
                    ParseGetPeopleAndRecipientPersonByIdMessagesNodesSender(element, "sender"),
                    DeserializeDateTime(element, "sent"),
                    DeserializeString(element, "text")
                );

            }

            return list;
        }

        private IParticipant ParseGetPeopleAndRecipientPersonByIdMessagesNodesRecipient(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Participant
            (
                DeserializeID(obj, "id"),
                DeserializeString(obj, "name"),
                DeserializeBoolean(obj, "isOnline")
            );
        }

        private IParticipant ParseGetPeopleAndRecipientPersonByIdMessagesNodesSender(
            JsonElement parent,
            string field)
        {
            JsonElement obj = parent.GetProperty(field);

            return new Participant
            (
                DeserializeID(obj, "id"),
                DeserializeString(obj, "name"),
                DeserializeBoolean(obj, "isOnline")
            );
        }

        private string DeserializeID(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_iDSerializer.Deserialize(value.GetString());
        }

        private string DeserializeString(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (string)_stringSerializer.Deserialize(value.GetString());
        }

        private System.Uri DeserializeNullableUrl(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (System.Uri)_urlSerializer.Deserialize(value.GetString());
        }

        private bool DeserializeBoolean(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (bool)_booleanSerializer.Deserialize(value.GetBoolean());
        }

        private System.DateTimeOffset DeserializeDateTime(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (System.DateTimeOffset)_dateTimeSerializer.Deserialize(value.GetString());
        }
        private Direction DeserializeDirection(JsonElement obj, string fieldName)
        {
            JsonElement value = obj.GetProperty(fieldName);
            return (Direction)_directionSerializer.Deserialize(value.GetString());
        }
    }
}
