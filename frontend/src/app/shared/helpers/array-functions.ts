export class ArrayFunctions {
    static addElementToArrayAsSecondToLast<T>(arr: T[], elementToAdd: T): T[] {
        const indexToAdd = arr.length - 1;

        arr.splice(indexToAdd, 0, elementToAdd);

        return arr;
    }
}
