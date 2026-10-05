// connection.js

import amqp from "amqplib";
import express from "express";
import dotenv from "dotenv";

dotenv.config();

let connection = null;

async function getConnection() {
    if (connection) return connection;

    connection = await amqp.connect(process.env.RABBITMQ_URL);

    return connection;
}

// channel.js

let channel = null;

async function getChannel() {

    if (channel) {
        return channel;
    }

    const connection = await getConnection();

    console.log("Creating channel...");

    channel = await connection.createChannel();

    console.log("Channel created");

    return channel;
}

const app=express();
app.listen(3000, () => {
    console.log("Server started");
});
app.get("/test", async (req, res) => {

    const channel = await getChannel();

    res.json({
        success: true
    });

});