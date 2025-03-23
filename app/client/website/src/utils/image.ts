type ImageFile = {
  data_url: string;
  file: File;
};

export const encodeImages = async (images: (ImageFile | string)[]): Promise<string[]> => {
  const encodedImages: string[] = [];

  for (const image of images) {
    if (typeof image === "string") {
      encodedImages.push(image);
    } else {
      const encodedImage = await readFileAsDataURL(image.file);
      encodedImages.push(encodedImage);
    }
  }

  return encodedImages;
};

const readFileAsDataURL = (file: File): Promise<string> => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();

    reader.onloadend = () => {
      resolve(reader.result as string);
    };

    reader.onerror = error => {
      reject(error);
    };

    reader.readAsDataURL(file);
  });
};
