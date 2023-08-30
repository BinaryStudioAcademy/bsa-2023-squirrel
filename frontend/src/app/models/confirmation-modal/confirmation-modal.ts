export interface ConfirmationModalInterface {
    cancelButtonLabel: string;
    confirmButtonLabel: string;
    modalHeader: string;
    modalDescription: string;
    callbackMethod: () => void;
}