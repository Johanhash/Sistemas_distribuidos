package peer

import (
	"bufio"
	"fmt"
	"net"
	"os"
)

var username string;

func StartListening(port string, user string) {
	username = user;
	listener, err := net.Listen("tcp", ":" + port)
	if err != nil {
		fmt.Println("Error listening:", err.Error())
		return
	}

	defer listener.Close()
	fmt.Printf("Peer is listening on port %v ...\n", port)
	for{
		conn, err := listener.Accept()
		if err != nil {
			fmt.Println("Error accepting conections: ", err.Error())
			continue
		}
		go receiveMessage(conn)
		sendMessage(conn)
	}
}

func ConnectToPeer(address string, user string) {
	username = user;
	conn, err := net.Dial("tcp", address)
	if err != nil {
		fmt.Println("Error connecting to peer: ", err.Error())
		return
	}
	defer conn.Close()

	go receiveMessage(conn)
	sendMessage(conn)
}

func receiveMessage(conn net.Conn) {
	defer conn.Close()
	reader := bufio.NewReader(conn)
	for{
		message, _ := reader.ReadString('\n')
		fmt.Print(message)
	}
}

func sendMessage(conn net.Conn) {
	writer := bufio.NewWriter(conn)
	scanner := bufio.NewScanner(os.Stdin)
	fmt.Println("Connected to peer. Type your message: ")
	for scanner.Scan() {
	
	message := scanner.Text()
	
	Message := fmt.Sprintf("%s: %s\n", username, message)
	
	_, err := writer.WriteString(Message)
	
	if err != nil {
		fmt.Println("Error sending message: ", err.Error())
	}
	writer.Flush()

	}
}