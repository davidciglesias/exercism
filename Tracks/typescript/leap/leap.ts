const isLeapYear = (year: number) => year % 4 === 0 && (year % 25 !== 0 || year % 400 === 0);

export default isLeapYear;
