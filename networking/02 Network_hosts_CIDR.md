# Networks, Hosts & CIDR

## Network
- A collection of connected devices.
- Enables communication between devices.
- Example: Home Wi-Fi (LAN).

## Host
- Any device in a network.
- Examples:
  - Laptop
  - Mobile
  - Server
  - Printer

## DHCP
- Router assigns IP addresses automatically.
- Ensures every host gets a unique IP.

## IP Structure
- IP = Network Part + Host Part

Example:
/24

192.168.1.25

Network -> 192.168.1
Host -> 25

## CIDR
Format:

192.168.1.0/24

- /24 = First 24 bits are network bits.
- Remaining 8 bits are host bits.

## Host Capacity

Formula:

Usable Hosts = 2^(Host Bits) - 2

Example:

/24

Host bits = 8

2^8 - 2 = 254 hosts

## Reserved Addresses
- Network Address → Host bits = 0
- Broadcast Address → Host bits = 1

## Remember
- Smaller CIDR (/16) → More hosts
- Larger CIDR (/28) → Fewer hosts
- CIDR determines network size.