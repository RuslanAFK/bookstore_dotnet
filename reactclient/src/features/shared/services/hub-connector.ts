import * as signalR from "@microsoft/signalr";
import {HubConnection} from "@microsoft/signalr";

class HubConnector {
    private connection: HubConnection;
    public subscribe: Function;
    private static instance: HubConnector;
    private constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7180/hubs/books")
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => console.error(err));
        this.subscribe = (onBookChanged: Function) => {
            this.connection.on("ChangedBooks", () => {
                onBookChanged();
            });
        };
    }
    public updateBook = () => {
        this.connection.invoke("UpdateBook")
            .catch(error => console.error(error));
    }
    public unsubscribe = () => {
        this.connection.off("ChangedBooks");
    }
    static getInstance() {
        if (!HubConnector.instance)
            HubConnector.instance = new HubConnector();
        return HubConnector.instance;
    }
}
export default HubConnector.getInstance;