import { SizeType } from "./product";

export class PendingDonation {
    category!: string;
    productName!: string;
    quantity!: number;
    sizeType: SizeType | undefined;
    size: string | undefined;
    unitName!: string;
    recordedAt!: Date;
    userName: string | undefined;
  }