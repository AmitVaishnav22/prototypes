const express = require("express");
const cors = require("cors");
const { spawn } = require("child_process");

const app = express();

app.use(cors());
app.use(express.json());

app.post("/execute", async (req, res) => {

    const {
        url,
        method = "GET",
        headers = {},
        body = null
    } = req.body;

    const args = [
        "-X",
        method,
        url
    ];

    // headers
    for (const key in headers) {
        args.push("-H");
        args.push(`${key}: ${headers[key]}`);
    }

    // body
    if (body && method !== "GET") {

        args.push("-H");
        args.push("Content-Type: application/json");

        args.push("-d");
        args.push(JSON.stringify(body));
    }

    console.log("Executing curl with args:", args.join(" "));

    const curl = spawn("curl", args);

    let output = "";
    let error = "";

    curl.stdout.on("data", (data) => {
        console.log("stdout chunk:", data.toString());
        output += data.toString();
    });

    curl.stderr.on("data", (data) => {
        console.log("stderr chunk:", data.toString());
        error += data.toString();
    });

    curl.on("close", () => {
        res.json({
            output,
            error
        });
    });
});

app.listen(3001, () => {
    console.log("Server running on port 3001");
});