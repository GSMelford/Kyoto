﻿using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Telegram.Updates;

public class Update
{
    public int UpdateId { get; set; }
    public Message? Message { get; set; }
}