import * as signalR from "@microsoft/signalr";
import {HubConnection} from "@microsoft/signalr";
import {notify} from "./toast-notifier";
import {API_URL} from "../store/urls";

class UserHubConnector {
    private connection: HubConnection;
    public subscribe: Function;
    private static instance: UserHubConnector;
    private static token: string | undefined;
    private constructor(token: string) {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_URL}hubs/users`, {
                accessTokenFactory: async () => token
            })
            .withAutomaticReconnect()
            .build();
        this.connection.start()
            .catch(err => notify(err, "warning"));
        this.subscribe = (onChangeFunc: Function) => {
            this.connection.on("UpdatedRole", (message: string) => {
                onChangeFunc(message);
            });
            this.connection.on("UserDeleted", (message: string) => {
                onChangeFunc(message);
            });
        };
    }
    public changeRole = (username: string, role: string) => {
        this.connection.invoke("ChangeRole", username, role)
            .catch(error => notify(error, "warning"));
    }

    public deleteUser = (username: string) => {
        this.connection.invoke("DeleteUser", username)
            .catch(error => notify(error, "warning"));
    }

    public unsubscribe = () => {
        this.connection.off("UpdatedRole");
        this.connection.off("UserDeleted");
        this.connection.stop()
            .catch(error => console.error(error));
    }
    static getInstance(newToken: string): UserHubConnector {
        if (!UserHubConnector.instance || UserHubConnector.token !== newToken) {
            UserHubConnector.token = newToken;
            UserHubConnector.instance = new UserHubConnector(UserHubConnector.token);
        }
        return UserHubConnector.instance;
    }
    static oldInstance(): UserHubConnector {
        return UserHubConnector.instance;
    }
}

export const old = UserHubConnector.oldInstance;
export default UserHubConnector.getInstance;
