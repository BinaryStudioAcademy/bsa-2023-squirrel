export class BranchNameFormatter {
    static formatBranchName(name: string) {
        return name.replace(/ /g, '-');
    }
}
