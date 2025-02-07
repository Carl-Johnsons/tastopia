import { Avatar } from "@rneui/base";
import { useCallback, useMemo } from "react";
import { View } from "react-native";
import UploadImage from "./UploadImage";
import { transformPlatformURI } from "@/utils/functions";
import { selectUser } from "@/slices/user.slice";
import { useAppDispatch } from "@/store/hooks";
import { saveIsDirtyFieldsData, saveUpdateProfileData } from "@/slices/menu/profile/updateProfileForm.slice";

export default function ImageChangingSection() {
  const { avatarUrl, backgroundUrl } = selectUser();
  const dispatch = useAppDispatch();

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
          dispatch(saveUpdateProfileData({ background: file }));
          dispatch(saveIsDirtyFieldsData({ background: true }));
          break;

        case "avatar":
          dispatch(saveUpdateProfileData({ avatar: file }));
          dispatch(saveIsDirtyFieldsData({ avatar: true }));
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
            className: "rounded-full bottom-0 right-0",
            style: {
              width: 30,
              height: 30
            },
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
