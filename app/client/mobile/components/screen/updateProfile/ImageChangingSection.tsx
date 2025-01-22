import { Avatar } from "@rneui/base";
import { useCallback, useMemo } from "react";
import { View } from "react-native";
import UploadImage from "./UploadImage";
import { transformPlatformURI } from "@/utils/functions";
import useUpdateProfile from "@/hooks/components/screen/updateProfile/useUpdateProfile";
import { selectUser } from "@/slices/user.slice";
import { stringify } from "@/utils/debug";

export default function ImageChangingSection() {
  const { setAvatar, setBackground } = useUpdateProfile();
  const { avatarUrl, backgroundUrl } = selectUser();

  const defaultBackgroundImages: ImageFileType[] | undefined = useMemo(() => {
    return backgroundUrl
      ? [
          {
            uri: backgroundUrl,
            name: backgroundUrl.split("/").pop() as string,
            type: "image/" + backgroundUrl.split(".").pop()
          }
        ]
      : undefined;
  }, [backgroundUrl]);

  const defaultAvatarImages: ImageFileType[] | undefined = useMemo(() => {
    return avatarUrl
      ? [
          {
            uri: avatarUrl,
            name: avatarUrl.split("/").pop() as string,
            type: "image/" + avatarUrl.split(".").pop()
          }
        ]
      : undefined;
  }, [avatarUrl]);

  const handleChangeFile = useCallback(
    (files: ImageFileType[], type: "background" | "avatar") => {
      const file = files[0];

      switch (type) {
        case "background":
          setBackground(file);
          break;

        case "avatar":
          setAvatar(file);
          break;
      }
    },
    []
  );

  return (
    <View className='relative h-[22vh]'>
      <UploadImage
        defaultImages={defaultBackgroundImages}
        innerImageClassName='aspect-[2.5]'
        onFileChange={files => handleChangeFile(files, "background")}
        replaceImageMode={{ enabled: true, globalCallback: true }}
      />

      <View className='bg-white_black absolute bottom-[-20px] left-[1.5rem] flex aspect-square w-[100px] items-center justify-center rounded-full'>
        <UploadImage
          defaultImages={defaultAvatarImages}
          onFileChange={files => handleChangeFile(files, "avatar")}
          replaceImageMode={{
            enabled: true,
            globalCallback: true,
            className: "rounded-full bottom-0 right-0 w-[31px] h-[32px]",
            replaceImageOnPress: true,
            iconSize: {
              width: 20,
              height: 20
            }
          }}
          renderImage={({ previewPath }) => {
            return (
              <Avatar
                size={90}
                rounded
                source={
                  previewPath
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
