# Routing

## What is Routing?
- Process of forwarding packets between different networks.
- Routers decide the next hop using the destination IP.

---

## Switch vs Router

| Switch | Router |
|---------|--------|
| Layer 2 | Layer 3 |
| Uses MAC Address | Uses IP Address |
| Connects devices in same LAN | Connects different networks |
| Forwards Frames | Forwards Packets |

---

# Scenario 1: Same Network

Laptop A (10.0.0.3)

↓

Laptop D (10.0.0.5)

1. Check subnet.
2. Same subnet
3. ARP Request:
   "Who has 10.0.0.5?"
4. Receive MAC Address.
5. Store in ARP Cache.
6. Switch forwards frame.

Frame

Src MAC = Laptop A

Dst MAC = Laptop D

Packet

Src IP = 10.0.0.3

Dst IP = 10.0.0.5

---

# Scenario 2: Different Networks

Laptop C

10.0.0.4

↓

Laptop X

192.168.1.5

1. Compare subnet.
2. Different subnet
3. Send packet to Default Gateway.
4. ARP for router's MAC.
5. Router forwards packet.
6. Router ARPs for destination MAC.
7. Frame delivered.

Important:

IP Address stays same.

MAC Address changes.

---

# Scenario 3: Internet Communication

Laptop

↓

Router

↓

ISP

↓

Internet

↓

AWS

Laptop creates

Frame
Packet
Segment

↓

Router removes Frame

↓

Performs NAT

↓

Creates new Frame

↓

Next Router

↓

...

↓

AWS Router

↓

EC2

---

# Default Gateway

- Used when destination is outside local network.
- Usually your router.

Example

192.168.1.1

---

# ARP

Purpose

IP → MAC

Example

Who has 192.168.1.20?

↓

Returns

MAC Address

↓

Stored in ARP Cache

---

# NAT (Network Address Translation)

Private IP

192.168.1.15

↓

Router

↓

Public IP

103.x.x.x

Response

103.x.x.x

↓

Router

↓

192.168.1.15

Purpose

- Allows multiple devices to share one public IP.
- Private IPs cannot travel on the Internet.

---

# Address Changes

Within Same LAN

IP → Same

MAC → Same source & destination

Across Routers

IP → Remains same

MAC → Changes at every hop

---

# Tables

ARP Table

IP ↔ MAC

Routing Table

Destination Network → Next Hop

---

# Packet Encapsulation

Application Data

↓

TCP Segment

↓

IP Packet

↓

Ethernet Frame

↓

Bits

---

# Remember

- Switch → Layer 2
- Router → Layer 3
- Switch forwards Frames.
- Router forwards Packets.
- ARP maps IP → MAC.
- Default Gateway handles external traffic.
- NAT converts Private IP ↔ Public IP.
- IP remains end-to-end.
- MAC changes at every hop.