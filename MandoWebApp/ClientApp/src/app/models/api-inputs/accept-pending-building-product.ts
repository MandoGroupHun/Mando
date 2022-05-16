export interface AcceptPendingBuildingProduct {
    pendingBuildingProductId: number | undefined;
    categoryId: number;
    huProductName: string | undefined;
    enProductName: string | undefined;
    buildingId: number;
    quantity: number;
    sizeTypeId: number | undefined;
    size: string | undefined;
    unitId: number;
    productId: number | undefined;
}
