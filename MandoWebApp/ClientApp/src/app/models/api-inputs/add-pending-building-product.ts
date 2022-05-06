export interface AddPendingBuildingProduct {
    buildingId: number;
    huProductName: string | undefined;
    enProductName: string | undefined;
    quantity: number;
    size: string | undefined;
    categoryId: number;
    sizeType: string | undefined;
    unitId: number;
}
