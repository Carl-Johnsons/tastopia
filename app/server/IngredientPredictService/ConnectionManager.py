from collections import defaultdict
from fastapi import WebSocket
import logging.config

class ConnectionManager:
    def __init__(self):
        # Dictionary mapping user_id to a list of WebSocket connections
        self.active_connections: dict[str, list[WebSocket]] = defaultdict(list)

    async def connect(self, websocket: WebSocket, user_id: str):
        await websocket.accept()
        self.active_connections[user_id].append(websocket)
        logging.info(f'{user_id} has {self.active_connections.__len__()} connections')

    def disconnect(self, websocket: WebSocket, user_id: str):
        self.active_connections[user_id].remove(websocket)
        # Remove the user_id key if no connections remain
        if not self.active_connections[user_id]:
            del self.active_connections[user_id]

    async def send_personal_message(self, message: str, user_id: str):
        for connection in self.active_connections.get(user_id, []):
            await connection.send_text(message)

    async def broadcast(self, message: str):
        for connections in self.active_connections.values():
            for connection in connections:
                await connection.send_text(message)
