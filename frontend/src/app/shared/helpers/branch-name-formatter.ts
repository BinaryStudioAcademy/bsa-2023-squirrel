export class BranchNameFormatter {
    static formatBranchName(name: string) {
        let formattedName = name.replace(/\s+/g, '-');

        formattedName = formattedName.replace(/[-_]+/g, '-');

        return formattedName;
    }

    static trimBranchName(name: string) {
        const trimmedName = name.replace(/^[-_ ]+|[-_ ]+$/g, '');

        return trimmedName;
    }
}
