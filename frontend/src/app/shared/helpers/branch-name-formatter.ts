export class BranchNameFormatter {
    static formatBranchName(name: string) {
        return name.trim().replace(/ /g, '-');
    }
}
