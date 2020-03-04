class ReverseString {
    static reverse(text: string = "") {
        return Array.from(text).reverse().join("").toString();
    }
}

export default ReverseString
