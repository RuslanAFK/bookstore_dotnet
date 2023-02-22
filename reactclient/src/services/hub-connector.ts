import * as signalR from "@microsoft/signalr";
import {HubConnection} from "@microsoft/signalr";

class HubConnector {
    connection: HubConnection;
    subscribe;
    static instance: HubConnector;
    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7180/hubs/books")
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => console.error(err));
        this.subscribe = (onBookChanged: any) => {
            this.connection.on("ChangedBooks", () => {
                onBookChanged();
            });
        };
    }
    updateBook = () => {
        this.connection.invoke("UpdateBook");
    }
    unsubscribe = () => {
        this.connection.off("ChangedBooks");
    }
    static getInstance() {
        if (!HubConnector.instance)
            HubConnector.instance = new HubConnector();
        return HubConnector.instance;
    }
}
export default HubConnector.getInstance;