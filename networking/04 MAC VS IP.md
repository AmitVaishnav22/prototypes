# IP vs MAC Addresses

## What is an IP Address?
- Logical address of a device.
- Assigned by DHCP or configured manually.
- Can change when the device joins a different network.
- Used for communication across networks (Internet).

Example: 192.168.1.10

---

## What is a MAC Address?
- MAC = Media Access Control Address.
- Physical (hardware) address of a Network Interface Card (NIC).
- Usually assigned by the manufacturer.
- Used for communication inside a Local Area Network (LAN).

Example: 00:1A:2B:3C:4D:5E

---

## IP vs MAC

| IP Address | MAC Address |
|------------|-------------|
| Logical Address | Physical Address |
| Layer 3 (Network Layer) | Layer 2 (Data Link Layer) |
| 32 bits (IPv4) | 48 bits |
| Decimal | Hexadecimal |
| Can change | Usually permanent |
| Assigned by DHCP/Admin | Assigned by Manufacturer |
| Used across networks | Used within a LAN |

---

## MAC Address Structure

48 bits = 6 Bytes

Example: 00:1A:2B:3C:4D:5E

### First 24 bits
- OUI (Organizationally Unique Identifier)
- Identifies the manufacturer.

### Last 24 bits
- Unique identifier assigned by the manufacturer.

---

## Network Interfaces
Every network interface has its own MAC address.

Examples:
- Ethernet
- Wi-Fi
- Bluetooth

---

## Why Both Are Needed

### IP Address
- Identifies **where** the device is.
- Routers use IP addresses to forward packets across networks.

### MAC Address
- Identifies **which hardware** should receive the frame on the local network.
- Switches use MAC addresses for local delivery.

Think of it as:

- **IP = Destination Address (City, Street)**
- **MAC = Person's Name at that Address**

---

## Packet Flow

Same LAN

Device A 
↓

Uses ARP to get Device B's MAC

↓

Sends frame directly to Device B

Different Network

Device A
↓

Sends packet to Default Gateway

↓

Routers forward using IP

↓

Last Router uses ARP

↓

Destination MAC found

↓

Frame delivered

---

## ARP (Address Resolution Protocol)

Purpose:

IP Address → MAC Address

Example:

Need to send to

192.168.1.20

↓

ARP Request

"Who has 192.168.1.20?"

↓

Device replies

"My MAC is 00:1A:2B:3C:4D:5E"

---

## Remember

- IP identifies the device's network location.
- MAC identifies the physical network interface.
- IP is used by routers.
- MAC is used by switches.
- IP changes; MAC usually doesn't.
- ARP maps IP → MAC.
- Every hop uses IP for routing, but each local Ethernet frame uses MAC addresses.

---