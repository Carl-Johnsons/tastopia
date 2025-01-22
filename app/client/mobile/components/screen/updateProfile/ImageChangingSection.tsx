import { selectUser } from "@/slices/user.slice";
import { Avatar } from "@rneui/base";
import { useCallback, useState } from "react";
import { View } from "react-native";
import UploadImage from "./UploadImage";
import { transformPlatformURI } from "@/utils/functions";

export default function ImageChangingSection() {
  const { backgroundUrl, avatarUrl } = selectUser();

  const defaultBackgroundImage: ImageFileType[] = backgroundUrl
    ? [
        {
          uri: backgroundUrl,
          name: backgroundUrl.split("/").pop() as string,
          type: "image/" + backgroundUrl.split(".").pop()
        }
      ]
    : [];

  const defaultAvatarImage: ImageFileType[] = avatarUrl
    ? [
        {
          uri: avatarUrl,
          name: avatarUrl.split("/").pop() as string,
          type: "image/" + avatarUrl.split(".").pop()
        }
      ]
    : [];

  const [background, setBackground] = useState<ImageFileType[]>(defaultBackgroundImage);
  const [avatar, setAvatar] = useState<ImageFileType[]>(defaultAvatarImage);

  const handleChangeFile = useCallback(
    (files: ImageFileType[], type: "background" | "avatar") => {
      switch (type) {
        case "background":
          setBackground(files);
          break;

        case "avatar":
          setAvatar(files);
          break;
      }
    },
    []
  );

  return (
    <View className='relative h-[22vh]'>
      <UploadImage
        defaultImages={background}
        innerImageClassName='aspect-[2.5]'
        onFileChange={files => handleChangeFile(files, "background")}
        replaceImageMode={{ enabled: true, globalCallback: true }}
      />

      <View className='bg-white_black absolute bottom-[-20px] left-[1.5rem] flex aspect-square w-[100px] items-center justify-center rounded-full'>
        <UploadImage
          defaultImages={avatar}
          onFileChange={files => handleChangeFile(files, "avatar")}
          replaceImageMode={{
            enabled: true,
            globalCallback: true,
            replaceImageOnPress: true,
            iconSize: {
              width: 20,
              height: 20
            },
            className: "rounded-full bottom-0 right-0 w-[30px] h-[30px]"
          }}
          renderImage={({ previewPath }) => {
            return (
              <Avatar
                size={90}
                rounded
                source={
                  avatarUrl
                    ? { uri: transformPlatformURI(previewPath) }
                    : require("../../../assets/images/avatar.png")
                }
                containerStyle={previewPath && { backgroundColor: "#FFC529" }}
              />
            );
          }}
        />
      </View>
    </View>
  );
}
