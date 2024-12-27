export class RageAPI {
    static callClient(eventName, ...args) {
        console.log(`Trigger: ${eventName} with args => ${args}`);
        if (window.mp) {
            window.mp.trigger(eventName, ...args);
        }
    }

    static invoke(name, ...args) {
        console.log(`Invoke: ${name} with args => ${args}`);
        if (window.mp) {
            window.mp.invoke(name, ...args);
        }
        /*

        Enables/disables local player cursor in game
        mp.invoke(“focus”, true/false);

        Used to set synchronized typing in chat state for local player (mp.players.local.isTypingInTextChat)
        mp.invoke("setTypingInChatState", true/false);
        
        Send command to server from local player
        mp.invoke("command", text);

        Send chat message to server from local player
        mp.invoke("chatMessage", text);

        */
    }
}