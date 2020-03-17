const allergy = {
    eggs: 1,
    peanuts: 2,
    shellfish: 4,
    strawberries: 8,
    tomatoes: 16,
    chocolate: 32,
    pollen: 64,
    cats: 128
};

type AllergyOption = keyof typeof allergy;

export default class Allergies {
    private readonly allergyLevel: number;
    constructor(allergyLevel: number) {
        this.allergyLevel = allergyLevel;
    }

    allergicTo = (option: AllergyOption): boolean =>
        (this.allergyLevel & allergy[option]) === (allergy[option] | allergy[option]);

    list = (): string[] =>
        (Object.keys(allergy) as AllergyOption[]).reduce<string[]>(
            (list, allergyItem) => (this.allergicTo(allergyItem) ? [...list, allergyItem] : list),
            []
        );
}
