export class PendingDonation {
    pendingDonationId!: number;
    category!: string;
    categoryId!: number;
    buildingId!: number;
    productName: string | undefined;
    huProductName: string | undefined;
    enProductName: string | undefined;
    quantity!: number;
    sizeTypeId: number | undefined;
    sizeTypeName: string | undefined;
    size: string | undefined;
    unitId!: number;
    unitName!: string;
    recordedAt!: Date;
    userName: string | undefined;
  }