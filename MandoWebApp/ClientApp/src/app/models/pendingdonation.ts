import { SizeType } from "./product";

export class PendingDonation {
    pendingDonationId!: number;
    category!: string;
    productName: string | undefined;
    huProductName: string | undefined;
    enProductName: string | undefined;
    quantity!: number;
    sizeType: SizeType | undefined;
    size: string | undefined;
    unitId!: number;
    unitName!: string;
    recordedAt!: Date;
    userName: string | undefined;
  }