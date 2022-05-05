export interface AcceptPendingBuildingProduct {
    pendingBuildingProductId: number | undefined;
    categoryId: number;
    huProductName: string | undefined;
    enProductName: string | undefined;
    buildingId: number;
    quantity: number;
    sizeType: string | undefined;
    size: string | undefined;
    unitId: number;
    productId: number | undefined;
}
