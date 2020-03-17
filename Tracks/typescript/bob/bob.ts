class Bob {
    private isYelling = (message: string): boolean =>
        message.match(/[A-Z]/g) !== null && message === message.toUpperCase();

    hey(message: string) {
        const parsedMessage = message.trim();
        if (parsedMessage.length === 0) return "Fine. Be that way!";
        const isYelling = this.isYelling(parsedMessage);
        if (parsedMessage.endsWith("?")) {
            return isYelling ? "Calm down, I know what I'm doing!" : "Sure.";
        }
        return isYelling ? "Whoa, chill out!" : "Whatever.";
    }
}

export default Bob;
