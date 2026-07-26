# OSI vs TCP/IP Model

## Why Layers?
- Simplifies networking.
- Modular architecture.
- Each layer has a specific responsibility.
- Changes in one layer don't affect others.

---

# OSI Model (7 Layers)

| Layer | Purpose | Common Protocols | Data Unit |
|--------|---------|------------------|-----------|
| 7. Application | User services | HTTP, HTTPS, FTP, DNS, SMTP | Data |
| 6. Presentation | Encryption, Compression, Encoding | TLS/SSL | Data |
| 5. Session | Session management | RPC, NetBIOS | Data |
| 4. Transport | Reliable delivery, Ports | TCP, UDP | Segment / Datagram |
| 3. Network | Routing, Logical Addressing | IP, ICMP | Packet |
| 2. Data Link | MAC Addressing | Ethernet, ARP | Frame |
| 1. Physical | Signals over medium | Wi-Fi, Fiber, Ethernet | Bits |

---

# TCP/IP Model (5 Layers)

| TCP/IP Layer | OSI Equivalent |
|--------------|----------------|
| Application | Application + Presentation + Session |
| Transport | Transport |
| Internet | Network |
| Data Link | Data Link |
| Physical | Physical |

> TCP/IP is the model used in real-world networking.

---

# Layer Responsibilities

## Application
- HTTP
- HTTPS
- FTP
- DNS
- SMTP

Provides services to applications.

---

## Presentation
- Encryption
- Compression
- Encoding

Example:
HTTPS uses TLS.

---

## Session
- Creates sessions.
- Maintains authentication.
- Session recovery.

---

## Transport
Protocols:
- TCP
- UDP

Responsibilities:
- Segmentation
- Reliability
- Flow Control
- Port Numbers

Data Unit:
Segment (TCP)

Datagram (UDP)

---

## Network
Protocols:
- IP
- ICMP

Responsibilities:
- Logical Addressing
- Routing
- Path Selection

Data Unit:
Packet

---

## Data Link
Protocols:
- Ethernet
- ARP

Responsibilities:
- MAC Addressing
- Frame Creation
- Error Detection

Data Unit:
Frame

---

## Physical
Responsibilities:
- Convert bits into electrical/radio/light signals.

Examples:
- Ethernet Cable
- Wi-Fi
- Fiber Optic

Data Unit:
Bits

---

# Encapsulation

Application Data

↓

TCP Header

↓

IP Header

↓

MAC Header

↓

Bits over Wire/Wi-Fi

---

# HTTP Request Flow

React App

↓

HTTP Request

↓

TCP Segment

↓

IP Packet

↓

Ethernet/Wi-Fi Frame

↓

Physical Signal

↓

Router

↓

Destination Server

↓

Node.js Application

---

# Router

- Operates at Layer 3.
- Reads IP Address.
- Chooses next hop.
- Doesn't inspect application data.

---

# Data Units

| Layer | Data Unit |
|--------|-----------|
| Application | Data |
| Transport | Segment / Datagram |
| Network | Packet |
| Data Link | Frame |
| Physical | Bits |

---

# Remember

- HTTP → Application Layer
- TCP/UDP → Transport Layer
- IP → Network Layer
- ARP/Ethernet → Data Link Layer
- Wi-Fi/Fiber/Cables → Physical Layer

- Router → Layer 3
- Switch → Layer 2

- OSI = Learning Model
- TCP/IP = Practical Model