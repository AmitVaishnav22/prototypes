# ICMP, Ping & Traceroute

## ICMP
- ICMP = Internet Control Message Protocol.
- Layer 3 (Network Layer) protocol.
- Used for network diagnostics and error reporting.
- Does NOT use ports.

---

## Purpose of ICMP

- Host Unreachable
- Network Unreachable
- Port Unreachable
- Time Exceeded (TTL expired)
- Echo Request
- Echo Reply

---

# Ping

Purpose:
- Checks whether a host is reachable.
- Measures Round Trip Time (RTT).

Flow

Sender

↓

ICMP Echo Request

↓

Destination

↓

ICMP Echo Reply

↓

Sender

Measures:

- Reachability
- Latency
- Packet Loss

Command

ping google.com

---

# Traceroute

Purpose

- Finds the path packets take.
- Displays every router (hop).

Command

Windows

tracert google.com

Linux/macOS

traceroute google.com

---

# TTL (Time To Live)

- Stored inside the IP header.
- Prevents infinite routing loops.
- Decremented by 1 at every router.

Example

TTL = 4

Router 1 → 3

Router 2 → 2

Router 3 → 1

Router 4 → 0

↓

Packet Dropped

↓

ICMP Time Exceeded sent back

---

# How Traceroute Works

TTL = 1

↓

Router 1

↓

TTL becomes 0

↓

ICMP Time Exceeded

↓

Router 1 discovered

------------------

TTL = 2

↓

Router 1

↓

Router 2

↓

TTL becomes 0

↓

ICMP Time Exceeded

↓

Router 2 discovered

Continue until destination replies.

---

# ICMP Errors

Host Unreachable
- Destination device cannot be reached.

Network Unreachable
- No route exists.

Port Unreachable
- Host reached.
- Requested service not available.

Time Exceeded
- TTL reached zero.

---

# Why "***" Appears in Traceroute

Possible reasons

- Router blocks ICMP.
- Firewall blocks ICMP.
- Router configured not to reply.

Packet may still continue forwarding.

---

# Layer

| Protocol | Layer |
|----------|-------|
| ICMP | Network (Layer 3) |
| IP | Network (Layer 3) |
| TCP | Transport (Layer 4) |
| UDP | Transport (Layer 4) |

---

# Remember

- ICMP is NOT used to transfer application data.
- ICMP is used for diagnostics and error reporting.
- Ping uses ICMP Echo Request/Reply.
- Traceroute uses increasing TTL values.
- Every router decreases TTL by 1.
- TTL = 0 → Packet dropped.
- Router sends ICMP Time Exceeded.