# TCP (Transmission Control Protocol)

## What is TCP?

- TCP = Transmission Control Protocol.
- Transport Layer (Layer 4) protocol.
- Connection-oriented.
- Reliable and ordered communication.

---

# Characteristics

- Connection-oriented.
- Reliable delivery.
- Ordered delivery.
- Acknowledgements (ACK).
- Retransmissions.
- Flow control.
- Congestion control.
- Larger header than UDP.

---

# TCP Header

| Field | Size |
|--------|------|
| Source Port | 2 Bytes |
| Destination Port | 2 Bytes |
| Sequence Number | 4 Bytes |
| Acknowledgement Number | 4 Bytes |
| Data Offset | 4 Bits |
| Reserved | 4 Bits |
| Flags | 12 Bits |
| Window Size | 2 Bytes |
| Checksum | 2 Bytes |
| Urgent Pointer | 2 Bytes |

Minimum Header = **20 Bytes**

Maximum Header = **60 Bytes** (with Options)

---

# Data Unit

TCP → Segment

---

# Important Flags

## SYN
- Starts a TCP connection.

## ACK
- Acknowledges received data.

## FIN
- Gracefully closes the connection.

## RST
- Immediately terminates the connection.

## PSH
- Immediately delivers buffered data to the application.

## URG
- Indicates urgent data.

## ECE
- Network congestion detected.

## CWR
- Sender reduced congestion window.

---

# Sequence Number

- Identifies the first byte in a segment.
- Maintains packet ordering.
- Helps retransmit missing data.

---

# Acknowledgement Number

- Indicates the next expected byte.
- Confirms successful reception.

Example

Received bytes

1–1000

↓

ACK = 1001

---

# Window Size

Purpose

Flow Control

Receiver advertises how much data it can currently accept.

Prevents sender from overwhelming the receiver.

---

# Checksum

Purpose

Error Detection

Receiver recalculates checksum.

If checksum doesn't match

↓

Segment discarded.

---

# Data Offset

Indicates where payload begins.

Minimum Value = 5

↓

20-byte TCP Header

If Options exist

↓

Header becomes larger.

---

# TCP Options

Examples

- MSS (Maximum Segment Size)
- Window Scaling
- Timestamp
- SACK (Selective Acknowledgement)

Maximum Optional Size = **40 Bytes**

---

# MSS (Maximum Segment Size)

Largest payload sent in one TCP segment.

Typical MSS

1460 Bytes

(MTU 1500 − IP Header 20 − TCP Header 20)

---

# Packet Flow

Application Data

↓

TCP Header

↓

IP Header

↓

Ethernet Header

↓

Bits

---

# TCP Features

Reliable Delivery

↓

Sequence Numbers

↓

Acknowledgements

↓

Retransmissions

↓

Flow Control

↓

Congestion Control

---

# TCP Used By

- HTTP
- HTTPS
- SSH
- FTP
- SMTP
- IMAP
- POP3

---

# Remember

- TCP is connection-oriented.
- Data Unit = Segment.
- Minimum Header = 20 Bytes.
- Maximum Header = 60 Bytes.
- Sequence Number maintains order.
- ACK confirms received data.
- Window Size provides Flow Control.
- Checksum detects errors.
- SYN starts a connection.
- FIN closes gracefully.
- RST closes immediately.