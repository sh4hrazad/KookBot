namespace KookBot.Enums; 

public enum WebsocketSignal {
        Event = 0,
        Hello = 1,
        Ping = 2,
        Pong = 3,
        Resume = 4,
        Reconnect = 5,
        ResumeAck = 6,
}
