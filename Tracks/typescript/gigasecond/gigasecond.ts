class GigaSecond {
    originalDate: Date;

    constructor(date: Date) {
        this.originalDate = date;
    }

    date(): Date {
        return new Date(this.originalDate.getTime() + Math.pow(10, 12));
    }
}

export default GigaSecond;