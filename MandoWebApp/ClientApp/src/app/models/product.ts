export class Product {
    productId!: number;
    category!: string;
    unitName!: string;
    sizeType: SizeType | undefined;
    name!: string;
  }

  export enum SizeType {
    Numbered = 'Numbered',
    TShirt = 'TShirt',
    Child = 'Child'
  }