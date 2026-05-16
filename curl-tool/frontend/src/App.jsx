import { useState } from "react";
import axios from "axios";

export default function App() {

  const [method, setMethod] = useState("GET");

  const [url, setUrl] = useState(
    "https://httpbin.org/post"
  );

  const [body, setBody] = useState(
    JSON.stringify({
      name: "A",
      role: "D"
    }, null, 2)
  );

  const [response, setResponse] = useState("");

  const executeCurl = async () => {

    try {

      const res = await axios.post(
        "http://localhost:3001/execute",
        {
          method,
          url,
          body: JSON.parse(body)
        }
      );

      let formattedOutput;

      try {

        // parse curl response body
        const parsed = JSON.parse(res.data.output);

        // beautify json
        formattedOutput = JSON.stringify(parsed, null, 2);

      } catch {

        // fallback if response is not json
        formattedOutput = res.data.output;

      }

      setResponse(formattedOutput);

    } catch (err) {

      setResponse(err.message);

    }
  };

  return (
    <div style={{
      padding: 30,
      fontFamily: "Arial"
    }}>

      <h1>Curl Tool</h1>

      <div style={{ marginBottom: 20 }}>

        <select
          value={method}
          onChange={(e) => setMethod(e.target.value)}
          style={{
            padding: 10,
            marginRight: 10
          }}
        >
          <option>GET</option>
          <option>POST</option>
          <option>PUT</option>
          <option>DELETE</option>
        </select>

        <input
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          style={{
            width: "600px",
            padding: "10px"
          }}
        />

      </div>

      <textarea
        value={body}
        onChange={(e) => setBody(e.target.value)}
        rows={10}
        style={{
          width: "700px",
          padding: 20
        }}
      />

      <br /><br />

      <button
        onClick={executeCurl}
        style={{
          padding: "12px 20px"
        }}
      >
        Execute
      </button>

      <pre style={{
        marginTop: 30,
        background: "#eee",
        padding: 20,
        whiteSpace: "pre-wrap"
      }}>
        {response}
      </pre>

    </div>
  );
}