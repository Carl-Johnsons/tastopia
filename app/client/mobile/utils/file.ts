import * as FileSystem from "expo-file-system";

/**
 * The function `extensionToMimeType` maps file extensions to their corresponding MIME types.
 * @param {string} extension file's extension
 * @returns the MIME type associated with the provided file
 */
const extensionToMimeType = (extension: string) => {
  const mimeTypes: { [key: string]: string } = {
    jpg: "image/jpeg",
    jpeg: "image/jpeg",
    png: "image/png",
    gif: "image/gif",
    bmp: "image/bmp",
    webp: "image/webp",
    svg: "image/svg+xml",
    pdf: "application/pdf",
    txt: "text/plain",
    html: "text/html",
    css: "text/css",
    js: "application/javascript",
    json: "application/json",
    xml: "application/xml",
    mp3: "audio/mpeg",
    wav: "audio/wav",
    mp4: "video/mp4"
  };

  return mimeTypes[extension.toLowerCase()] || "application/octet-stream";
};

const isOnlineImage = (url: string) => {
  if (typeof url !== "string" || !url.trim()) return false;

  const urlPattern = /^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|webp)(\?.*)?)$/i;

  return urlPattern.test(url);
};

const getFileInfo = async (fileURI: string) => {
  const fileInfo = await FileSystem.getInfoAsync(fileURI);
  return fileInfo;
};

const isLessThanTheMB = (fileSize: number, smallerThanSizeMB: number) => {
  const isOk = fileSize / 1024 / 1024 < smallerThanSizeMB;
  return isOk;
};

export { extensionToMimeType, isOnlineImage, getFileInfo, isLessThanTheMB };
