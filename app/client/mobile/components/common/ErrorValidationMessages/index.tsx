import React from "react";
import { View, Text, StyleSheet, StyleProp, TextStyle } from "react-native";

type ErrorValidationMessagesProps = {
  errorMessages: string[];
  styles?: StyleProp<TextStyle>;
};

const ErrorValidationMessages = ({
  errorMessages,
  styles
}: ErrorValidationMessagesProps) => {
  return (
    <View style={styles}>
      {errorMessages.map((msg, index) =>
        msg ? (
          <Text
            style={[stylesDefault.text, styles && styles]}
            key={`error-msg-${index}`}
          >{`*${msg}`}</Text>
        ) : null
      )}
    </View>
  );
};

export default ErrorValidationMessages;

const stylesDefault = StyleSheet.create({
  text: {
    color: "red",
    marginTop: 8,
    fontSize: 12
  }
});
